using System.ComponentModel.DataAnnotations;

namespace Portfolio.Entity;

public class Tag
{
    [Required] public int Id { get; set; }
    [Required] public string Name { get; set; } = null!;
}
