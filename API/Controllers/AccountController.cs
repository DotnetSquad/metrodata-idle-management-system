using API.DataTransferObjects.Accounts;
using API.Services;
using API.Utilities.Enums;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = $"{nameof(RoleLevelEnum.HR)}")]
public class AccountController : ControllerBase
{
    private readonly AccountService _accountService;

    public AccountController(AccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var accounts = _accountService.Get();

        if (!accounts.Any())
        {
            return NotFound(new ResponseHandler<AccountDtoGet>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "No accounts found",
                Data = null
            });
        }

        return Ok(new ResponseHandler<IEnumerable<AccountDtoGet>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "accounts found",
            Data = accounts
        });
    }

    [HttpGet("{guid}")]
    public IActionResult Get(Guid guid)
    {
        var account = _accountService.Get(guid);

        if (account is null)
        {
            return NotFound(new ResponseHandler<AccountDtoGet>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Account not found",
                Data = null
            });
        }

        return Ok(new ResponseHandler<AccountDtoGet>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Account found",
            Data = account
        });
    }

    [HttpPost]
    public IActionResult Create(AccountDtoCreate accountDtoCreate)
    {
        var accountCreated = _accountService.Create(accountDtoCreate);

        if (accountCreated is null)
        {
            return BadRequest(new ResponseHandler<AccountDtoCreate>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Account not created",
                Data = null
            });
        }

        return Ok(new ResponseHandler<AccountDtoCreate>
        {
            Code = StatusCodes.Status201Created,
            Status = HttpStatusCode.Created.ToString(),
            Message = "Account created",
            Data = accountCreated
        });
    }

    [HttpPut]
    public IActionResult Update(AccountDtoUpdate accountDtoUpdate)
    {
        var accountUpdated = _accountService.Update(accountDtoUpdate);

        if (accountUpdated == -1)
        {
            return NotFound(new ResponseHandler<AccountDtoUpdate>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Account not found",
                Data = null
            });
        }

        if (accountUpdated == 0)
        {
            return BadRequest(new ResponseHandler<AccountDtoUpdate>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Account not updated",
                Data = null
            });
        }

        return Ok(new ResponseHandler<AccountDtoUpdate>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Account updated",
            Data = accountDtoUpdate
        });
    }

    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        var accountDeleted = _accountService.Delete(guid);

        if (accountDeleted == -1)
        {
            return NotFound(new ResponseHandler<AccountDtoGet>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Account not found",
                Data = null
            });
        }

        if (accountDeleted == 0)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseHandler<AccountDtoGet>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Account not deleted",
                Data = null
            });
        }

        return Ok(new ResponseHandler<AccountDtoGet>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Account deleted",
            Data = null
        });
    }

    [AllowAnonymous]
    [HttpPost("ChangePassword")]
    public IActionResult ChangePassword(AccountDtoChangePassword accountDtoChangePassword)
    {
        var isUpdated = _accountService.ChangePassword(accountDtoChangePassword);
        if (isUpdated == 0)
            return NotFound(new ResponseHandler<AccountDtoUpdate>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Email not found"
            });

        if (isUpdated == -1)
        {
            return BadRequest(new ResponseHandler<AccountDtoUpdate>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Otp is already used"
            });
        }

        if (isUpdated == -2)
        {
            return BadRequest(new ResponseHandler<AccountDtoUpdate>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Otp is incorrect"
            });
        }

        if (isUpdated == -3)
        {
            return BadRequest(new ResponseHandler<AccountDtoUpdate>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Otp is expired"
            });
        }

        if (isUpdated is -4)
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseHandler<AccountDtoUpdate>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Error retrieving data from the database"
            });

        return Ok(new ResponseHandler<AccountDtoUpdate>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Password has been changed successfully"
        });
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public IActionResult Login(AccountDtoLogin accountDtoLogin)
    {
        var loginStatus = _accountService.Login(accountDtoLogin);
        if (loginStatus == "0")
            return NotFound(new ResponseHandler<AccountDtoGet>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Account not found",
                Data = null
            });

        if (loginStatus == "-1")
            return BadRequest(new ResponseHandler<AccountDtoGet>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Password is incorrect",
                Data = null
            });

        if (loginStatus == "-2")
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseHandler<AccountDtoGet>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Error retrieving when creating token",
                Data = null
            });
        }

        return Ok(new ResponseHandler<string>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Login Success",
            Data = loginStatus
        });
    }

    [AllowAnonymous]
    [HttpPost("ForgotPassword")]
    public IActionResult ForgotPassword(AccountDtoForgotPassword accountDtoForgotPassword)
    {
        var isUpdated = _accountService.ForgotPassword(accountDtoForgotPassword);
        if (isUpdated == 0)
            return NotFound(new ResponseHandler<AccountDtoGet>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Email not found",
                Data = null
            });

        if (isUpdated is -1)
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseHandler<AccountDtoGet>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Error retrieving data from the database",
                Data = null
            });

        return Ok(new ResponseHandler<AccountDtoGet>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Otp has been sent to your email",
            Data = null
        });
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public IActionResult Register(AccountDtoRegister accountDtoRegister)
    {
        var isCreated = _accountService.RegisterAccount(accountDtoRegister);
        if (!isCreated)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseHandler<AccountDtoRegister>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Register failed"
            });
        }

        return Ok(new ResponseHandler<AccountDtoRegister>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Register Success"
        });
    }
}
