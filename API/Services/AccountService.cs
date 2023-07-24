using API.Contracts;
using API.DataTransferObjects.Accounts;
using API.Models;
using API.Utilities.Handlers;

namespace API.Services;

public class AccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IEmployeeRepository _employeeRepository;

    public AccountService(IAccountRepository accountRepository,
                          IEmployeeRepository employeeRepository)
    {
        _accountRepository = accountRepository;
        _employeeRepository = employeeRepository;
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
        return !accountUpdated ? 0 : 1;
    }

    public int Delete(Guid guid)
    {
        var account = _accountRepository.GetByGuid(guid);
        if (account is null) return -1;

        var accountDeleted = _accountRepository.Delete(account);
        return !accountDeleted ? 0 : 1;
    }

    public int ChangePassword(ChangePasswordDto changePasswordDto)
    {
        var employee = _employeeRepository.GetEmployeeByEmail(changePasswordDto.Email);
        if (employee is null)
            return 0;

        var account = _accountRepository.GetByGuid(employee.Guid);
        if (account is null)
            return 0;

        if (account.IsUsed)
            return -1;

        if (account.Otp != changePasswordDto.Otp)
            return -2;

        if (account.ExpiredTime < DateTime.Now)
            return -3;

        var isUpdated = _accountRepository.Update(new Account
        {
            Guid = account.Guid,
            Password = HashingHandler.HashPassword(changePasswordDto.NewPassword),
            IsDeleted = account.IsDeleted,
            Otp = account.Otp,
            ExpiredTime = account.ExpiredTime,
            IsUsed = true,
            CreatedDate = account.CreatedDate,
            ModifiedDate = DateTime.Now
        });

        return isUpdated ? 1 : -4;
    }
}
