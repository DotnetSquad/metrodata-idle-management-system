using Client.DataTransferObjects.Accounts;
using Client.Repositories;
using Client.Utilities.Handlers;

namespace Client.Contracts;

public interface IAccountRepository : IBaseRepository<AccountDtoGet, Guid>
{
    public Task<ResponseHandler<string>> Login(AccountDtoLogin accountDtoLogin);
    public Task<ResponseHandler<AccountDtoRegister>> Register(AccountDtoRegister entity);
    public Task<ResponseHandler<AccountDtoForgotPassword>> ForgotPassword(AccountDtoForgotPassword entity);
    public Task<ResponseHandler<AccountDtoChangePassword>> ChangePassword(AccountDtoChangePassword entity);
}
