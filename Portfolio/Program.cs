using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Portfolio.DataAccess;
using Portfolio.Entity;
using Portfolio.Misc.Services.EmailSender;
using Portfolio.Services;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

//database
services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MsSqlConnection"), action =>
        action.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName)));

services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

//email
services
    .AddSingleton(builder.Configuration.GetSection(nameof(EmailConfiguration)).Get<EmailConfiguration>())
    .AddSingleton<IEmailService, EmailService>()
    .AddSingleton<IEmailSender, EmailSenderIdentityAdapter>();

services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = new PathString("/Auth/Login");
    options.AccessDeniedPath = new PathString("/Home/AccessDenied");//TODO
});

//pages, mvc
services.AddRazorPages();
services.AddControllersWithViews();

var app = builder.Build();

(app.Environment.IsDevelopment()
        ? app
        : app.UseExceptionHandler("/Home/Error").UseHsts())
    .UseHttpsRedirection()
    .UseStaticFiles()
    .UseRouting()
    .UseAuthentication()
    .UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
