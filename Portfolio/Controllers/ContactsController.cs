using Microsoft.AspNetCore.Mvc;
using Portfolio.DataAccess;
using Portfolio.Entity;
using Portfolio.Misc.Services.EmailSender;

namespace Portfolio.Controllers;

public class ContactsController : Controller
{
    private readonly IEmailService _emailService;
    private readonly ApplicationDbContext _dataContext;

    public ContactsController(IEmailService emailService, ApplicationDbContext dataContext)
    {
        _emailService = emailService;
        _dataContext = dataContext;
    }

    [HttpGet]
    public IActionResult Index() =>
        View();

    public record MessageModel(string Name, string Email, string Subject, string Message);

    [HttpPost]
    public IActionResult SendMessage([FromForm] MessageModel message1)
    {
        _dataContext.Requests.Add(new Request(0, message1.Name, message1.Email, message1.Subject, message1.Message));
        _dataContext.SaveChanges();
        //_emailService.SendMessageAsync(message1.Email, message1.Message, message1.Name, message1.Subject);
        return RedirectToAction("Index");
    }
}