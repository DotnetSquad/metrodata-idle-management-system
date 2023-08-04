using API.Models;

namespace API.Contracts;

public interface IPlacementRepository : IBaseRepository<Placement>
{
    public IEnumerable<Placement> GetByEmployeeGuid(Guid guid);
}
