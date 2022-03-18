using Microsoft.AspNetCore.Identity.UI.Services;
using Portfolio.Misc.Services.EmailSender;

namespace Portfolio.Services;

public class EmailSenderIdentityAdapter : IEmailSender
{
    private readonly IEmailService _emailService;

    public EmailSenderIdentityAdapter(IEmailService emailService) =>
        _emailService = emailService;

    public async Task SendEmailAsync(string email, string subject, string htmlMessage) => 
        await _emailService.SendMessageAsync(email, htmlMessage, "qwe", subject);
}
