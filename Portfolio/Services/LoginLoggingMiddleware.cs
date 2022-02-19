namespace Portfolio.Services;

public class LoginLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public LoginLoggingMiddleware(RequestDelegate next) =>
        _next = next;

    public async Task Invoke(HttpContext context)
    {
        Console.WriteLine($"new connection on {context.Request.Path}");
        Console.WriteLine(context.Connection.RemoteIpAddress);
        await _next(context);
    }
}