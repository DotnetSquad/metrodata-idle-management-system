using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

public class PlacementRepository : BaseRepository<Placement>, IPlacementRepository
{
    public PlacementRepository(ApplicationDbContext context) : base(context)
    {
    }
}
