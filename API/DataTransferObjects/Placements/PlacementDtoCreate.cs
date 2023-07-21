namespace API.DataTransferObjects.Placements;

public class PlacementDtoCreate
{
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid EmployeeGuid { get; set; }
    public Guid CompanyGuid { get; set; }
}
