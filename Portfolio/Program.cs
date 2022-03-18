using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Portfolio.DataAccess;
using Portfolio.Misc.Services.EmailSender;
using Portfolio.Services;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

//database
services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MsSqlConnection"), action =>
        action.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName)));

services.AddAuthentication(options =>
{
    options.DefaultScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
}).AddIdentityCookies();
services.AddIdentityCore<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddDefaultUI()
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<ApplicationDbContext>();

//email
services
    .AddSingleton<IEmailService>(EmailService.GetInstance(
        builder.Configuration.GetSection(nameof(EmailConfiguration)).Get<EmailConfiguration>()))
    .AddSingleton<IEmailSender, EmailSenderIdentityAdapter>();

//pages, mvc
services.AddRazorPages();
services.AddControllersWithViews();

var app = builder.Build();

(app.Environment.IsDevelopment()
        ? app.UseMigrationsEndPoint()
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
