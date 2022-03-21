using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using testMvc.Database;
using testMvc.Entities;
using testMvc.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MsSqlConnection")));
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationContext>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

(app.Environment.IsDevelopment()
        ? app
        : app.UseHsts().UseExceptionHandler("/Home/Error"))
    .UseStaticFiles()
    .UseRouting()
    .UseAuthentication()
    .UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
