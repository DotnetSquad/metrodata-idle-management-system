using API.Contracts;
using API.DataTransferObjects.Placements;

namespace API.Services;

public class PlacementService
{
    private readonly IPlacementRepository _placementRepository;

    public PlacementService(IPlacementRepository placementRepository)
    {
        _placementRepository = placementRepository;
    }

    public IEnumerable<PlacementDtoGet> Get()
    {
        var placements = _placementRepository.GetAll().ToList();
        if (!placements.Any()) return Enumerable.Empty<PlacementDtoGet>();
        List<PlacementDtoGet> placementDtoGets = new();

        foreach (var placement in placements)
        {
            placementDtoGets.Add((PlacementDtoGet)placement);
        }

        return placementDtoGets;
    }

    public PlacementDtoGet? Get(Guid guid)
    {
        var placement = _placementRepository.GetByGuid(guid);
        if (placement is null) return null!;

        return (PlacementDtoGet)placement;
    }

    public PlacementDtoCreate? Create(PlacementDtoCreate placementDtoCreate)
    {
        var placementCreated = _placementRepository.Create(placementDtoCreate);
        if (placementCreated is null) return null!;

        return (PlacementDtoCreate)placementCreated;
    }

    public int Update(PlacementDtoUpdate placementDtoUpdate)
    {
        var placement = _placementRepository.GetByGuid(placementDtoUpdate.Guid);
        if (placement is null) return -1;

        var placementUpdated = _placementRepository.Update(placementDtoUpdate);
        return !placementUpdated ? 0 : 1;
    }

    public int Delete(Guid guid)
    {
        var placement = _placementRepository.GetByGuid(guid);
        if (placement is null) return -1;

        var placementDeleted = _placementRepository.Delete(placement);
        return !placementDeleted ? 0 : 1;
    }
}
