using Microsoft.EntityFrameworkCore;
using Portfolio.Entity;

namespace Portfolio.DataAccess;

public class MyContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=portfolio;Integrated Security=True");

    public DbSet<Request> Requests { get; set; } = null!;
}