using System.Security.Claims;
using API.Contracts;
using API.DataTransferObjects.Accounts;
using API.Utilities.Handlers;

namespace API.Services;

public class AccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IAccountRoleRepository _accountRoleRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly ITokenHandler _tokenHandler;

    public AccountService(IAccountRepository accountRepository, IAccountRoleRepository accountRoleRepository,
        IEmployeeRepository employeeRepository, IRoleRepository roleRepository, ITokenHandler tokenHandler)
    {
        _accountRepository = accountRepository;
        _accountRoleRepository = accountRoleRepository;
        _employeeRepository = employeeRepository;
        _roleRepository = roleRepository;
        _tokenHandler = tokenHandler;
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

    public string Login(AccountDtoLogin accountDtoLogin)
    {
        var employee = _employeeRepository.GetByEmail(accountDtoLogin.Email);
        if (employee is null) return "0";

        var account = _accountRepository.GetByGuid(employee.Guid);
        if (account is null) return "0";


        if (!HashingHandler.ValidatePassword(accountDtoLogin.Password, account!.Password)) return "-1";

        try
        {
            var claims = new List<Claim>()
            {
                new Claim("NIK", employee.Nik),
                new Claim("FullName", $"{employee.FirstName} {employee.LastName}"),
                new Claim("EmailAddress", accountDtoLogin.Email)
            };

            var accountRoles = _accountRoleRepository.GetAccountRolesByAccountGuid(account.Guid);
            var getRolesNameByAccountRole = from accountRole in accountRoles
                join role in _roleRepository.GetAll() on accountRole.RoleGuid equals role.Guid
                select role.Name;

            var token = _tokenHandler.GenerateToken(claims);
            return token;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return "-2";
        }
    }
}
