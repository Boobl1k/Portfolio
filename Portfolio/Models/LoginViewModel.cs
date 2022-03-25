using System.ComponentModel.DataAnnotations;

namespace Portfolio.Models;

public class LoginViewModel
{
    [Required]
    public string Email { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;
    
    [Display(Name = "Remember me")]
    public bool RememberMe { get; set; }
         
    public string? ReturnUrl { get; set; }
}
