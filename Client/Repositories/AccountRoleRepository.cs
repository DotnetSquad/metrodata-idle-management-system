using Client.Contracts;
using Client.DataTransferObjects.AccountRoles;

namespace Client.Repositories;

public class AccountRoleRepository : BaseRepository<AccountRoleDtoGet, Guid>, IAccountRoleRepository
{
    public AccountRoleRepository(string request = "accountrole/") : base(request)
    {
    }
}
