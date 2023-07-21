namespace API.DataTransferObjects.Companies;

public class CompanyDtoUpdate
{
    public Guid Guid { get; set; }
    public string CompanyName { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }
}
