using System.ComponentModel.DataAnnotations;

namespace testMvc.Models;

public class RegisterViewModel
{
    [Required]
    public string Email { get; set; } = null!;

    [Required]
    [Display(Name = "Year of birth")]
    public int Year { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [Required]
    [Compare("Password", ErrorMessage = "Passwords are not same")]
    [DataType(DataType.Password)]
    [Display(Name = "Password one more time")]
    public string PasswordConfirm { get; set; } = null!;
}
