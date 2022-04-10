using System.ComponentModel.DataAnnotations;

namespace Portfolio.Entity;

public class Picture
{
    public int Id { get; set; }

    [Required]
    public byte[] Data { get; set; } = null!;
}
