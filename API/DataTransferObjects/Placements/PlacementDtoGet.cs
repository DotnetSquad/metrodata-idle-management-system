using API.Models;

namespace API.DataTransferObjects.Placements;

public class PlacementDtoGet
{
    public Guid Guid { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid EmployeeGuid { get; set; }
    public Guid CompanyGuid { get; set; }

    // implicit operator
    public static implicit operator Placement(PlacementDtoGet placementDtoGet)
    {
        return new Placement
        {
            Guid = placementDtoGet.Guid,
            Title = placementDtoGet.Title,
            Description = placementDtoGet.Description,
            EmployeeGuid = placementDtoGet.EmployeeGuid,
            CompanyGuid = placementDtoGet.CompanyGuid
        };
    }

    // explicit operator
    public static explicit operator PlacementDtoGet(Placement placement)
    {
        return new PlacementDtoGet
        {
            Guid = placement.Guid,
            Title = placement.Title,
            Description = placement.Description,
            EmployeeGuid = placement.EmployeeGuid,
            CompanyGuid = placement.CompanyGuid
        };
    }
}
