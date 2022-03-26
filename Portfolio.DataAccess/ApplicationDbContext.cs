using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Portfolio.Entity;

namespace Portfolio.DataAccess;

public class ApplicationDbContext : IdentityDbContext<User>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Request> Requests { get; set; } = null!;
    public DbSet<Post> Posts { get; set; } = null!;
    public DbSet<Tag> Tags { get; set; } = null!;
    public DbSet<PostTag> PostTag { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<PostTag>().HasKey(tag => new {tag.PostId, tag.TagId});
        builder.Entity<PostTag>()
            .HasOne(postTag => postTag.Post)
            .WithMany(post => post.PostTags)
            .HasForeignKey(postTag => postTag.PostId);  
        builder.Entity<PostTag>()
            .HasOne(postTag => postTag.Tag)
            .WithMany(tag => tag.PostTags)
            .HasForeignKey(postTag => postTag.TagId);
    }
}
