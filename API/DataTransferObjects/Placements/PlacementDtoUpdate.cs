using API.Models;

namespace API.DataTransferObjects.Placements;

public class PlacementDtoUpdate
{
    public Guid Guid { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid EmployeeGuid { get; set; }
    public Guid CompanyGuid { get; set; }

    // implicit operator
    public static implicit operator Placement(PlacementDtoUpdate placementDtoUpdate)
    {
        return new Placement
        {
            Guid = placementDtoUpdate.Guid,
            Title = placementDtoUpdate.Title,
            Description = placementDtoUpdate.Description,
            EmployeeGuid = placementDtoUpdate.EmployeeGuid,
            CompanyGuid = placementDtoUpdate.CompanyGuid
        };
    }

    // explicit operator
    public static explicit operator PlacementDtoUpdate(Placement placement)
    {
        return new PlacementDtoUpdate
        {
            Guid = placement.Guid,
            Title = placement.Title,
            Description = placement.Description,
            EmployeeGuid = placement.EmployeeGuid,
            CompanyGuid = placement.CompanyGuid
        };
    }
}
