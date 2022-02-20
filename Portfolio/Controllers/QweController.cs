using Microsoft.AspNetCore.Mvc;

namespace Portfolio.Controllers;

public class QweController  : Controller
{
    public IActionResult Index() => 
        View();
}