namespace API.DataTransferObjects.Dashboards;

public class DashboardDtoGetInterviewStatus
{
    public int Pending { get; set; }
    public int Accepted { get; set; }
    public int Rejected { get; set; }
}
