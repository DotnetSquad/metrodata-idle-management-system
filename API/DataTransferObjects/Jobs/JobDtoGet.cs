namespace API.DataTransferObjects.Jobs;

public class JobDtoGet
{
    public Guid Guid { get; set; }
    public string JobName { get; set; }
    public string Description { get; set; }
    public Guid CompanyGuid { get; set; }
}
