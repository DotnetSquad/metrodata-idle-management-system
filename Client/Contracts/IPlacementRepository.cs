using Client.DataTransferObjects.Placements;

namespace Client.Contracts;

public interface IPlacementRepository : IBaseRepository<PlacementDtoGet, Guid>
{
}
