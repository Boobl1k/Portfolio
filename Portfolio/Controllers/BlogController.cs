using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

    [HttpGet]
    public IActionResult Index() =>
        View(_dataContext.Posts);

    [HttpGet]
    public IActionResult Blog([FromQuery] int postId)
    {
        Console.WriteLine(postId);
        var post = _dataContext.Posts.FirstOrDefault(post => post.Id == postId);
        if (post is null) return BadRequest();
        var tags = _dataContext.Tags.Where(
            tag => _dataContext.PostTag.Where(
                    postTag => postTag.PostId == postId)
                .Any(postTag => postTag.TagId == tag.Id));
        return View(new BlogViewModel
        {
            Author = post.Author,
            Tags = tags,
            Title = post.Title,
            Text = post.Text,
            Date = post.Date
        });
    }

    [HttpGet]
    public IActionResult AddBlog() =>
        View(new AddPostViewModel
        {
            AuthorId = _userManager.GetUserId(User)
        });

    [HttpPost]
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
            Author = await _userManager.FindByIdAsync(_userManager.GetUserId(User)),
            Title = model.Title,
            Text = model.Text,
            Date = DateTime.Now
        };
        _dataContext.Posts.Add(post);
        await _dataContext.SaveChangesAsync();
        post.PostTags = tags.Union(newTags).Select(tag => new PostTag
        {
            PostId = post.Id,
            Post = post,
            TagId = tag.Id,
            Tag = tag
        }).ToList();
        await _dataContext.SaveChangesAsync();
        return RedirectToAction("Blog", new {postId = post.Id});
    }
}
