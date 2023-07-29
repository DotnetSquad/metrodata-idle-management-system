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

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(AccountDtoRegister register)
    {
        var result = await _accountRepository.Register(register);
        if (result is null)
        {
            return RedirectToAction("Error", "Home");
        }
        else if (result.Code == 400)
        {
            ModelState.AddModelError(string.Empty, result.Message);
            TempData["Error"] = $"Something Went Wrong! - {result.Message}!";
            return View();
        }
        else if (result.Code == 200)
        {
            TempData["Success"] = $"Data has been Successfully Registered! - {result.Message}!";
            return RedirectToAction("Login", "Account");
        }

        return View();
    }

    public IActionResult Index()
    {
        return View();
    }

    // forgot password feature
    public IActionResult ForgotPassword()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ForgotPassword(AccountDtoForgotPassword accountDtoForgotPassword)
    {
        var result = await _accountRepository.ForgotPassword(accountDtoForgotPassword);

        if (result is null)
        {
            return RedirectToAction("Error", "Home");
        }
        else if (result.Code == 400)
        {
            ModelState.AddModelError(string.Empty, result.Message);
            TempData["Error"] = $"Something Went Wrong! - {result.Message}!";
            return View();
        }
        else if (result.Code == 200)
        {
            TempData["Success"] = $"Data has been Successfully Registered! - {result.Message}!";
            return RedirectToAction("ChangePassword", "Account");
        }

        return View();
    }

    public IActionResult ChangePassword()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ChangePassword(AccountDtoChangePassword accountDtoChangePassword)
    {
        var result = await _accountRepository.ChangePassword(accountDtoChangePassword);

        if (result is null)
        {
            return RedirectToAction("Error", "Home");
        }
        else if (result.Code == 400)
        {
            ModelState.AddModelError(string.Empty, result.Message);
            TempData["Error"] = $"Something Went Wrong! - {result.Message}!";
            return View();
        }
        else if (result.Code == 200)
        {
            TempData["Success"] = $"Data has been Successfully Registered! - {result.Message}!";
            return RedirectToAction("Login", "Account");
        }

        return View();
    }
    
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }
}
