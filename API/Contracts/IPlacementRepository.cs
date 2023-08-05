using API.Models;

namespace API.Contracts;

public interface IPlacementRepository : IBaseRepository<Placement>
{
    public Placement GetByEmployeeGuid(Guid guid);
}
