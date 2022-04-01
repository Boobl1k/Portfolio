using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portfolio.DataAccess;
using Portfolio.Entity;
using Portfolio.Models;

namespace Portfolio.Controllers;

public class BlogController : Controller
{
    private readonly ApplicationDbContext _dataContext;
    private readonly UserManager<User> _userManager;

    public BlogController(ApplicationDbContext dataContext, UserManager<User> userManager)
    {
        _dataContext = dataContext;
        _userManager = userManager;
    }

    public record PostSearchModel(int? TagId, string? PostTitle);

    [HttpGet]
    public IActionResult Index([FromQuery] PostSearchModel postSearchModel)
    {
        var (tagId, postTitle) = postSearchModel;
        var posts = _dataContext.Posts.Include(post => post.Author) as IQueryable<Post>;
        posts = tagId is { }
            ? posts.Where(post => post.Tags!.Contains(new Tag {Id = (int) tagId,}))
            : posts;
        posts = postTitle is { }
            ? posts.Where(post => post.Title.Contains(postTitle))
            : posts;
        return View(posts);
    }

    [HttpGet]
    public IActionResult Blog([FromQuery] int postId)
    {
        var post = _dataContext.Posts
            .Include(post => post.Tags)
            .FirstOrDefault(post => post.Id == postId);
        if (post is null) return BadRequest();
        return View(new BlogViewModel
        {
            Author = post.Author,
            Tags = post.Tags!,
            Title = post.Title,
            Text = post.Text,
            Date = post.Date
        });
    }

    [HttpGet]
    [Authorize]
    public IActionResult AddBlog() =>
        View(new AddPostViewModel
        {
            AuthorId = _userManager.GetUserId(User)
        });

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddBlog(AddPostViewModel model)
    {
        if (!ModelState.IsValid) return RedirectToAction("Index", "Home");
        var tagNames = model.Tags.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var tags = _dataContext.Tags.Where(tag => tagNames.Contains(tag.Name)).ToList();
        var newTags = (from tagName in tagNames
            where tags.All(tag => tag.Name != tagName)
            select new Tag {Name = tagName}).ToList();
        _dataContext.Tags.AddRange(newTags);
        await _dataContext.SaveChangesAsync();
        var post = new Post
        {
            Author = await _userManager.FindByNameAsync(User.Identity!.Name),
            Title = model.Title,
            Text = model.Text,
            Date = DateTime.Now
        };
        _dataContext.Posts.Add(post);
        await _dataContext.SaveChangesAsync();
        post.Tags = tags.Union(newTags).ToList();
        await _dataContext.SaveChangesAsync();
        return RedirectToAction("Blog", new {postId = post.Id});
    }
}
