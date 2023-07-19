using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers;


public class AccountController : Controller
{
    public IActionResult Register()
    {
        return View();
    }

    public IActionResult Index()
    {
        return View();
    }
}