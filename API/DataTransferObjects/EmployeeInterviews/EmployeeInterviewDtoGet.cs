namespace API.DataTransferObjects.EmployeeInterviews;

public class EmployeeInterviewDtoGet
{
    public Guid Guid { get; set; }
    public Guid EmployeeGuid { get; set; }
    public Guid InterviewGuid { get; set; }
}
