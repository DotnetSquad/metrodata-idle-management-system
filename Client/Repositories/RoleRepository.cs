using Client.Contracts;
using Client.DataTransferObjects.Roles;

namespace Client.Repositories;

public class RoleRepository : BaseRepository<RoleDtoGet, Guid>, IRoleRepository
{
    public RoleRepository(string request = "Role/") : base(request)
    {
    }
}
