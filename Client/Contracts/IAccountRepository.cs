using Client.DataTransferObjects.Accounts;
using Client.Utilities.Handlers;

namespace Client.Contracts;

public interface IAccountRepository : IBaseRepository<AccountDtoRegister, string>
{
        public Task<ResponseHandler<string>> Login(AccountDtoLogin accountDtoLogin);
}
