using System.ComponentModel.DataAnnotations;

namespace Portfolio.Entity;

public class Post
{
    [Required] public int Id { get; set; }
    [Required] public User Author { get; set; } = null!;
    [Required] public ICollection<PostTag> PostTags { get; set; } = null!;
    [Required] public string Title { get; set; } = null!;
    [Required] public string Text { get; set; } = null!;
    [Required] public DateTime Date { get; set; }
}
