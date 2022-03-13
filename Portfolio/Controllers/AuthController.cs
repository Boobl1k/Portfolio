using Microsoft.AspNetCore.Mvc;

namespace Portfolio.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class AuthController : ControllerBase
{
    public record struct UserCred(string Login, string Password);
    
    [HttpPost]
    public IActionResult Register(UserCred userCred)
    {
        
        return Ok();
    }
}