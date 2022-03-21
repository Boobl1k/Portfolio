using Microsoft.AspNetCore.Identity;

namespace testMvc.Entities;

public class User : IdentityUser
{
    public int Year { get; set; }
}
