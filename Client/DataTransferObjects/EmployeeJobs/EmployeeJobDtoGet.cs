using Client.Utilities.Enums;

namespace Client.DataTransferObjects.EmployeeJobs;

public class EmployeeJobDtoGet
{
    public Guid Guid { get; set; }
    public Guid EmployeeGuid { get; set; }
    public Guid? InterviewGuid { get; set; }
    public Guid JobGuid { get; set; }
    public StatusApprovalEnum StatusApproval { get; set; }
}
