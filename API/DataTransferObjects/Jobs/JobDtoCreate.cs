namespace API.DataTransferObjects.Jobs;

public class JobDtoCreate
{
    public string JobName { get; set; }
    public string Description { get; set; }
    public Guid CompanyGuid { get; set; }
}
