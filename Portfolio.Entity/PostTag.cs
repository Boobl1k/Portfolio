using System.ComponentModel.DataAnnotations.Schema;

namespace Portfolio.Entity;

public class PostTag
{
    [Column("PostId")]
    public int PostId { get; set; }
    public Post Post { get; set; }
    [Column("TagId")]
    public int TagId { get; set; }
    public Tag Tag { get; set; }
}
