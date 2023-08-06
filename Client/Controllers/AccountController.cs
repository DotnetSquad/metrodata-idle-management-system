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
        var account = await _accountRepository.Login(accountDtoLogin);

        switch (account.Code)
        {
            case 200:
                HttpContext.Session.SetString("JWTToken", account.Data);
                return RedirectToAction("Index", "Dashboard");
            case 400:
                TempData["Error"] = account.Message;
                return RedirectToAction(nameof(Login));
            default:
                TempData["Error"] = "Login failed! Please ensure the email and password are valid.";
                return RedirectToAction(nameof(Login));
        }
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(AccountDtoRegister register)
    {
        var account = await _accountRepository.Register(register);

        switch (account.Code)
        {
            case 200:
                TempData["Success"] = account.Message;
                return RedirectToAction("Login", "Account");
            case 400:
                TempData["Error"] = account.Message;
                return RedirectToAction(nameof(Register));
            default:
                TempData["Error"] = "Registration failed: Invalid user input.";
                return RedirectToAction(nameof(Register));
        }
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
        var account = await _accountRepository.ForgotPassword(accountDtoForgotPassword);

        switch (account.Code)
        {
            case 200:
                TempData["Success"] = account.Message;
                return RedirectToAction("ChangePassword", "Account");
            case 400:
                TempData["Error"] = account.Message;
                return RedirectToAction(nameof(ForgotPassword));
            default:
                TempData["Error"] = "Forgot password failed: Invalid email address";
                return RedirectToAction(nameof(ForgotPassword));
        }
    }

    public IActionResult ChangePassword()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ChangePassword(AccountDtoChangePassword accountDtoChangePassword)
    {
        var account = await _accountRepository.ChangePassword(accountDtoChangePassword);

        switch (account.Code)
        {
            case 200:
                TempData["Success"] = account.Message;
                return RedirectToAction("Login", "Account");
            case 400:
                TempData["Error"] = account.Message;
                return RedirectToAction(nameof(ChangePassword));
            default:
                TempData["Error"] = "Password reset failed.";
                return RedirectToAction(nameof(ChangePassword));
        }
    }
    
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }
}
