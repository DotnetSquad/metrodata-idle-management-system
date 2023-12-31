﻿using API.Contracts;
using API.Data;
using API.DataTransferObjects.Accounts;
using API.Models;
using API.Utilities.Enums;
using API.Utilities.Handlers;
using System.Security.Claims;

namespace API.Services;

public class AccountService
{
    private readonly ApplicationDbContext _context;
    private readonly IAccountRepository _accountRepository;
    private readonly IAccountRoleRepository _accountRoleRepository;
    private readonly IEmailHandler _emailHandler;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IGradeRepository _gradeRepository;
    private readonly IProfileRepository _profileRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly ITokenHandler _tokenHandler;

    public AccountService(IAccountRepository accountRepository, IAccountRoleRepository accountRoleRepository,
        IEmailHandler emailHandler, IEmployeeRepository employeeRepository, IGradeRepository gradeRepository,
        IProfileRepository profileRepository,
        IRoleRepository roleRepository,
        ITokenHandler tokenHandler, ApplicationDbContext context)
    {
        _accountRepository = accountRepository;
        _accountRoleRepository = accountRoleRepository;
        _emailHandler = emailHandler;
        _employeeRepository = employeeRepository;
        _gradeRepository = gradeRepository;
        _profileRepository = profileRepository;
        _roleRepository = roleRepository;
        _tokenHandler = tokenHandler;
        _context = context;
    }

    public IEnumerable<AccountDtoGet> Get()
    {
        var accounts = _accountRepository.GetAll().ToList();
        if (!accounts.Any()) return Enumerable.Empty<AccountDtoGet>();
        List<AccountDtoGet> accountDtoGets = new();

        foreach (var account in accounts)
        {
            accountDtoGets.Add((AccountDtoGet)account);
        }

        return accountDtoGets;
    }

    public AccountDtoGet? Get(Guid guid)
    {
        var account = _accountRepository.GetByGuid(guid);
        if (account is null) return null!;

        return (AccountDtoGet)account;
    }

    public AccountDtoCreate? Create(AccountDtoCreate accountDtoCreate)
    {
        var accountCreated = _accountRepository.Create(accountDtoCreate);
        if (accountCreated is null) return null!;

        return (AccountDtoCreate)accountCreated;
    }

    public int Update(AccountDtoUpdate accountDtoUpdate)
    {
        var account = _accountRepository.GetByGuid(accountDtoUpdate.Guid);
        if (account is null) return -1;

        var accountUpdated = _accountRepository.Update(accountDtoUpdate);
        return accountUpdated ? 1 : 0;
    }

    public int Delete(Guid guid)
    {
        var account = _accountRepository.GetByGuid(guid);
        if (account is null) return -1;

        var accountDeleted = _accountRepository.Delete(account);
        return accountDeleted ? 1 : 0;
    }


    public int ChangePassword(AccountDtoChangePassword accountDtoChangePassword)
    {
        var employee = _employeeRepository.GetByEmail(accountDtoChangePassword.Email);
        if (employee is null)
            return 0;

        var account = _accountRepository.GetByGuid(employee.Guid);
        if (account is null)
            return 0;

        if (account.IsUsed)
            return -1;

        if (account.Otp != accountDtoChangePassword.Otp)
            return -2;

        if (account.ExpiredTime < DateTime.Now)
            return -3;

        var isUpdated = _accountRepository.Update(new Account
        {
            Guid = account.Guid,
            Password = HashingHandler.HashPassword(accountDtoChangePassword.NewPassword),
            IsDeleted = account.IsDeleted,
            Otp = account.Otp,
            ExpiredTime = account.ExpiredTime,
            IsUsed = true,
            CreatedDate = account.CreatedDate,
            ModifiedDate = DateTime.Now
        });

        return isUpdated ? 1 : -4;
    }

    public string Login(AccountDtoLogin accountDtoLogin)
    {
        var employee = _employeeRepository.GetByEmail(accountDtoLogin.Email);
        if (employee is null) return "0";
        var profile = _profileRepository.GetByGuid(employee.ProfileGuid);

        var account = _accountRepository.GetByGuid(employee.Guid);
        if (account is null) return "0";


        if (!HashingHandler.ValidatePassword(accountDtoLogin.Password, account!.Password)) return "-1";

        try
        {
            var claims = new List<Claim>()
            {
                new Claim("Guid", employee.Guid.ToString()),
                new Claim("FullName", $"{employee.FirstName} {employee.LastName}"),
                new Claim("PhotoProfile", profile.PhotoProfile),
                new Claim("GradeGuid", employee.GradeGuid.ToString() ?? Guid.Empty.ToString()),
                new Claim("ProfileGuid", employee.ProfileGuid.ToString() ?? Guid.Empty.ToString()),
            };

            var accountRoles = _accountRoleRepository.GetAccountRolesByAccountGuid(account.Guid);
            var getRolesNameByAccountRole = from accountRole in accountRoles
                                            join role in _roleRepository.GetAll() on accountRole.RoleGuid equals role.Guid
                                            select role.Name;
            claims.AddRange(getRolesNameByAccountRole.Select(role => new Claim(ClaimTypes.Role, role)));

            var token = _tokenHandler.GenerateToken(claims);
            return token;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return "-2";
        }
    }

    public int ForgotPassword(AccountDtoForgotPassword accountDtoForgotPassword)
    {
        var employee = _employeeRepository.GetByEmail(accountDtoForgotPassword.Email);
        if (employee is null)
            return 0; // Email not found

        var account = _accountRepository.GetByGuid(employee.Guid);
        if (account is null)
            return -1;

        var otp = new Random().Next(111111, 999999);
        var isUpdated = _accountRepository.Update(new Account
        {
            Guid = account.Guid,
            Password = account.Password,
            IsDeleted = account.IsDeleted,
            Otp = otp,
            ExpiredTime = DateTime.Now.AddMinutes(5),
            IsUsed = false,
            CreatedDate = account.CreatedDate,
            ModifiedDate = DateTime.Now
        });

        if (!isUpdated)
            return -1;

        _emailHandler.SendEmail(accountDtoForgotPassword.Email,
            "Forgot Password",
            $"Your OTP is {otp}");

        return 1;
    }

    public bool Register(AccountDtoRegister accountDtoRegister)
    {
        using var transaction = _context.Database.BeginTransaction();
        try
        {
            var grade = new Grade
            {
                Guid = Guid.NewGuid(),
                GradeLevel = GradeEnum.B,
                ScoreSegment1 = 0,
                ScoreSegment2 = 0,
                ScoreSegment3 = 0,
                ScoreSegment4 = 0,
                TotalScore = 0,
                CreatedDate = DateTime.Now
            };
            var gradeCreated = _gradeRepository.Create(grade);

            var profile = new Profile
            {
                Guid = Guid.NewGuid(),
                PhotoProfile = "",
                Skills = "",
                Linkedin = "",
                Resume = "",
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
            var profileCreated = _profileRepository.Create(profile);

            var employee = new Employee
            {
                Guid = new Guid(),
                FirstName = accountDtoRegister.FirstName,
                LastName = accountDtoRegister.LastName ?? "",
                BirthDate = accountDtoRegister.BirthDate,
                Gender = accountDtoRegister.Gender,
                HiringDate = accountDtoRegister.HiringDate,
                Email = accountDtoRegister.Email,
                PhoneNumber = accountDtoRegister.PhoneNumber,
                ProfileGuid = profileCreated.Guid,
                GradeGuid = gradeCreated.Guid,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
            };
            employee.Nik = GenerateHandler.GenerateNik(_employeeRepository.GetLastEmployeeNik());
            _employeeRepository.Create(employee);

            var account = new Account
            {
                Guid = employee.Guid,
                Password = HashingHandler.HashPassword(accountDtoRegister.Password),
                IsDeleted = false,
                IsUsed = false,
                Otp = 0,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                ExpiredTime = DateTime.Now.AddMinutes(10)
            };
            _accountRepository.Create(account);

            var roleEmployee = _roleRepository.GetByName("Employee");
            _accountRoleRepository.Create(new AccountRole
            {
                AccountGuid = account.Guid,
                RoleGuid = roleEmployee.Guid
            });

            transaction.Commit();
            return true;
        }
        catch
        {
            transaction.Rollback();
            return false;
        }
    }
}
