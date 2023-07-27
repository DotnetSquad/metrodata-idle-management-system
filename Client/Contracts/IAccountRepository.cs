using Client.DataTransferObjects.Accounts;
using Client.Repositories;
using Client.Utilities.Handlers;

namespace Client.Contracts;

public interface IAccountRepository : IBaseRepository<AccountDtoGet, Guid>
{
    public Task<ResponseHandler<string>> Login(AccountDtoLogin accountDtoLogin);
    Task<ResponseHandler<AccountRepository>> Register(AccountDtoRegister entity);
}
