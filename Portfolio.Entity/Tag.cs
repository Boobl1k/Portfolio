using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Portfolio.Entity;

[Index(nameof(Name))]
public class Tag
{
    [Required] public int Id { get; set; }
    [Required] public string Name { get; set; } = null!;
    public List<Post>? Posts { get; set; }
}
