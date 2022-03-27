using System.ComponentModel.DataAnnotations;

namespace Portfolio.Entity;

public class Post
{
    [Required] public int Id { get; set; }
    [Required] public string AuthorId { get; set; } = null!;
    [Required] public User Author { get; set; } = null!;
    public List<Tag>? Tags { get; set; }
    [Required] public string Title { get; set; } = null!;
    [Required] public string Text { get; set; } = null!;
    [Required] public DateTime Date { get; set; }
}
