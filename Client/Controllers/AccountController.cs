using Client.Contracts;
using Client.DataTransferObjects.Accounts;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers;

public class AccountController : Controller
{
    private readonly IAccountRepository _accountRepository;

    public AccountController(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(AccountDtoLogin accountDtoLogin)
    {
        var result = await _accountRepository.Login(accountDtoLogin);
        if (result is null)
        {
            return RedirectToAction("Error", "Home");
        }
        else if (result.Code == 400)
        {
            ModelState.AddModelError(string.Empty, result.Message);
            return View();
        }
        else if (result.Code == 200)
        {
            HttpContext.Session.SetString("JWTToken", result.Data);
            return RedirectToAction("Index", "Dashboard");
        }
        return View();
    }

    public IActionResult Register()
    {
        return View();
    }

    public IActionResult Index()
    {
        return View();
    }
}