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

        switch (account?.Code)
        {
            case null:
                return RedirectToAction("Error", "Home");
            case 200:
                HttpContext.Session.SetString("JWTToken", account.Data);
                return RedirectToAction("Index", "Dashboard");
            case 400:
                TempData["Error"] = account.Message;
                return RedirectToAction(nameof(Index));
            default:
                TempData["Error"] = account.Message;
                return RedirectToAction(nameof(Index));
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

        switch (account?.Code)
        {
            case null:
                return RedirectToAction("Error", "Home");
            case 200:
                TempData["Success"] = account.Message;
                return RedirectToAction("Login", "Account");
            case 400:
                TempData["Error"] = account.Message;
                return RedirectToAction(nameof(Index));
            default:
                TempData["Error"] = account.Message;
                return RedirectToAction(nameof(Index));
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

        switch (account?.Code)
        {
            case null:
                return RedirectToAction("Error", "Home");
            case 200:
                TempData["Success"] = account.Message;
                return RedirectToAction("ChangePassword", "Account");
            case 400:
                TempData["Error"] = account.Message;
                return RedirectToAction(nameof(Index));
            default:
                TempData["Error"] = account.Message;
                return RedirectToAction(nameof(Index));
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

        switch (account?.Code)
        {
            case null:
                return RedirectToAction("Error", "Home");
            case 200:
                TempData["Success"] = account.Message;
                return RedirectToAction("Login", "Account");
            case 400:
                TempData["Error"] = account.Message;
                return RedirectToAction(nameof(Index));
            default:
                TempData["Error"] = account.Message;
                return RedirectToAction(nameof(Index));
        }
    }
    
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }
}
