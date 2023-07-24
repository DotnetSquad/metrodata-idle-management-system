using API.Models;

namespace API.Contracts;

public interface IRoleRepository : IBaseRepository<Role>
{
    Role? GetByName(string name);
}
