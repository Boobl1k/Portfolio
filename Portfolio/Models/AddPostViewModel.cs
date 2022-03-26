using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Portfolio.Models;

public class AddPostViewModel
{
    [Required] [HiddenInput(DisplayValue = false)] public string AuthorId { get; set; } = null!;
    [Required] public string Tags { get; set; } = null!;
    [Required] public string Title { get; set; } = null!;
    [Required] public string Text { get; set; } = null!;
}
