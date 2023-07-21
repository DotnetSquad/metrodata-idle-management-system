using API.Models;

namespace API.DataTransferObjects.Placements;

public class PlacementDtoCreate
{
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid EmployeeGuid { get; set; }
    public Guid CompanyGuid { get; set; }

    // implicit operator
    public static implicit operator Placement(PlacementDtoCreate placementDtoCreate)
    {
        return new Placement
        {
            Title = placementDtoCreate.Title,
            Description = placementDtoCreate.Description,
            EmployeeGuid = placementDtoCreate.EmployeeGuid,
            CompanyGuid = placementDtoCreate.CompanyGuid
        };
    }

    // explicit operator
    public static explicit operator PlacementDtoCreate(Placement placement)
    {
        return new PlacementDtoCreate
        {
            Title = placement.Title,
            Description = placement.Description,
            EmployeeGuid = placement.EmployeeGuid,
            CompanyGuid = placement.CompanyGuid
        };
    }
}
