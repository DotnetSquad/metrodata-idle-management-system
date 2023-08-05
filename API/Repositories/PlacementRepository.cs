using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

public class PlacementRepository : BaseRepository<Placement>, IPlacementRepository
{
    public PlacementRepository(ApplicationDbContext context) : base(context)
    {
    }

    public Placement GetByEmployeeGuid(Guid guid)
    {
        return Context.Set<Placement>().FirstOrDefault(p => p.EmployeeGuid == guid);
    }
}
