using Client.Utilities.Enums;

namespace Client.DataTransferObjects.EmployeeInterviews;

public class EmployeeInterviewDtoCreate
{
    public Guid EmployeeGuid { get; set; }
    public Guid InterviewGuid { get; set; }
    public StatusApprovalEnum StatusApproval { get; set; }
}
