using Client.Contracts;
using Client.DataTransferObjects.Placements;

namespace Client.Repositories;

public class PlacementRepository : BaseRepository<PlacementDtoGet, Guid>, IPlacementRepository
{
    public PlacementRepository(string request = "Placement/") : base(request)
    {
    }
}
