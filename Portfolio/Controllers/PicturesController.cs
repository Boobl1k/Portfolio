using Microsoft.AspNetCore.Mvc;
using Portfolio.DataAccess;
using Portfolio.Entity;

namespace Portfolio.Controllers;

public class PicturesController : Controller
{
    private readonly ApplicationDbContext _dataContext;

    public PicturesController(ApplicationDbContext dataContext) =>
        _dataContext = dataContext;

    [HttpGet]
    public IActionResult Index() =>
        View(_dataContext.Pictures);

    [HttpGet]
    public IActionResult Create() => 
        View();

    [HttpPost]
    public async Task<ActionResult> Create(Picture pic, IFormFile? uploadImage)
    {
        if (uploadImage is null) return View(pic);
        using var reader = new BinaryReader(uploadImage.OpenReadStream());
        pic.Data = reader.ReadBytes((int) uploadImage.Length);
        _dataContext.Pictures.Add(pic);
        await _dataContext.SaveChangesAsync();
        return RedirectToAction("Index");
    }
}
