using System.ComponentModel.DataAnnotations;
using Portfolio.Entity;

namespace Portfolio.Models;

public class BlogViewModel
{
    [Required] public User? Author { get; set; }
    public List<Tag> Tags { get; set; } = null!;
    [Required] public string Title { get; set; } = null!;
    [Required] public string Text { get; set; } = null!;
    [Required] public DateTime Date { get; set; }
}
