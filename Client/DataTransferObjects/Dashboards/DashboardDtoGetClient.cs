namespace Client.DataTransferObjects.Dashboards;

public class DashboardDtoGetClient
{
    public Guid CompanyGuid { get; set; }
    public string CompanyName { get; set; }
    public int TotalEmployees { get; set; }
}
