using API.Models;

namespace API.Contracts;

public interface IAccountRoleRepository : IBaseRepository<AccountRole>
{
    IEnumerable<AccountRole> GetAccountRolesByAccountGuid(Guid guid);
}
