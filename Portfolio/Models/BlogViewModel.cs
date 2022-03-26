using Microsoft.Build.Framework;
using Portfolio.Entity;

namespace Portfolio.Models;

public class BlogViewModel
{
    [Required] public User Author { get; set; } = null!;
    [Required] public IQueryable<Tag> Tags { get; set; } = null!;
    [Required] public string Title { get; set; } = null!;
    [Required] public string Text { get; set; } = null!;
    [Required] public DateTime Date { get; set; }
}
