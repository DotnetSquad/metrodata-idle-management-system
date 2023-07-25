using Client.DataTransferObjects.Roles;

namespace Client.Contracts
{
    public interface IRoleRepository : IBaseRepository<RoleDtoGet, Guid>
    {
    }
}
