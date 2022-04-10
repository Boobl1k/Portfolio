using Microsoft.AspNetCore.Identity;

namespace Portfolio.Entity;

public class User : IdentityUser
{
    public int Year { get; set; }
    public Picture? Picture { get; set; }
}
