using Client.DataTransferObjects.Accounts;
using Client.Repositories;
using Client.Utilities.Handlers;

namespace Client.Contracts;

public interface IAccountRepository : IBaseRepository<AccountDtoGet, Guid>
{
    Task<ResponseHandler<AccountRepository>> Register(AccountDtoRegister entity);
}
