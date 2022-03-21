using System.Text;
using MailKit.Net.Smtp;
using MimeKit;

namespace Portfolio.Misc.Services.EmailSender;

public class EmailService : IEmailService, IDisposable
{
    private readonly EmailConfiguration _config;

    private readonly Task<Task> _connectionTask;
    private async Task Connect() => await await _connectionTask;

    public EmailService(EmailConfiguration config)
    {
        _config = config;
        _connectionTask = _client.ConnectAsync(_config.SmtpServer, _config.Port, true).ContinueWith(_ =>
            _client.AuthenticateAsync(_config.UserName, _config.Password));
    }

    private readonly SmtpClient _client = new();

    public async Task SendMessageAsync(string email, string message, string name, string subject,
        CancellationToken cancellationToken = default)
    {
        var mime = new MimeMessage();
        mime.From.Add(new MailboxAddress("qweqweqwe", _config.From));
        mime.To.Add(new MailboxAddress(name, email));
        mime.Subject = subject;

        var multipart = new Multipart("Mixed");
        multipart.Add(new TextPart(MimeKit.Text.TextFormat.Text) {Text = message});

        multipart.Add(new MimePart("text", "txt")
        {
            Content = new MimeContent(new MemoryStream(Encoding.UTF8.GetBytes(message))),
            ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
            ContentTransferEncoding = ContentEncoding.Base64,
            FileName = "text.txt"
        });

        mime.Body = multipart;

        await Connect();
        await _client.SendAsync(mime, cancellationToken);
    }

    public void Dispose()
    {
        _client.Dispose();
        GC.SuppressFinalize(this);
    }
}
