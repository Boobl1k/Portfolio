using MailKit.Net.Smtp;
using MimeKit;

namespace Portfolio.Misc.Services.EmailSender;

public class EmailService : IEmailService
{
    private static EmailService? _instance;
    private static readonly object locker = new();
    private static readonly Exception notConfiguredException = new("EmailService is not configured");
    public static EmailService Instance => _instance ?? throw notConfiguredException;

    public static EmailService GetInstance(EmailConfiguration? config = default)
    {
        if (_instance is { }) return _instance;
        lock (locker) return _instance ??= config is { } ? new EmailService(config) : throw notConfiguredException;
    }

    private readonly EmailConfiguration _config;
    private EmailService(EmailConfiguration config) => _config = config;

    public async Task SendMessageAsync(string email, string message, string name, string subject)
    {
        using var client = new SmtpClient();
        await client.ConnectAsync(_config.SmtpServer, _config.Port, true);
        client.AuthenticationMechanisms.Remove("XOAUTH2");
        await client.AuthenticateAsync(_config.UserName, _config.Password);

        var mime = new MimeMessage();
        mime.From.Add(new MailboxAddress("qweqweqwe", _config.From));
        mime.To.Add(new MailboxAddress(name, email));
        mime.Subject = subject;
        mime.Body = new TextPart(MimeKit.Text.TextFormat.Text) {Text = message};

        await client.SendAsync(mime);
    }
}
