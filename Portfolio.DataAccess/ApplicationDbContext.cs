using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Portfolio.Entity;

namespace Portfolio.DataAccess;

public class ApplicationDbContext : IdentityDbContext
{
    //public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(Settings.MsSqlConnectionString);
        base.OnConfiguring(optionsBuilder);
    }

    public DbSet<Request> Requests { get; set; } = null!;
}
