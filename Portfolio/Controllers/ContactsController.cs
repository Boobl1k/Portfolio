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
    public async Task<IActionResult> SendMessage([FromForm] MessageModel msg)
    {
        _dataContext.Requests.Add(new Request(0, msg.Name, msg.Email, msg.Subject, msg.Message));
        await _dataContext.SaveChangesAsync();
        await _emailService.SendMessageAsync(msg.Email, msg.Message, msg.Name, msg.Subject);
        return RedirectToAction("Index");
    }
}
