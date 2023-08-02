using Client.Utilities.Enums;

namespace Client.DataTransferObjects.EmployeeInterviews;

public class EmployeeInterviewDtoUpdate
{
    public Guid Guid { get; set; }
    public Guid EmployeeGuid { get; set; }
    public Guid InterviewGuid { get; set; }
    public StatusApprovalEnum StatusApproval { get; set; }
}
