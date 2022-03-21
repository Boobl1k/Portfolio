namespace Portfolio.Misc.Services.EmailSender;

public interface IEmailService
{
    public Task SendMessageAsync(string email, string message, string name, string subject,
        CancellationToken cancellationToken = default);
}
