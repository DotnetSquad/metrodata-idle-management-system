using API.Models;
using API.Utilities.Enums;

namespace API.DataTransferObjects.EmployeeJobs;

public class EmployeeJobDtoUpdate
{
    public Guid Guid { get; set; }
    public Guid EmployeeGuid { get; set; }
    public Guid InterviewGuid { get; set; }
    public Guid JobGuid { get; set; }
    public StatusApprovalEnum StatusApproval { get; set; }

    // implicit operator
    public static implicit operator EmployeeJob(EmployeeJobDtoUpdate employeeJobDtoUpdate)
    {
        return new EmployeeJob
        {
            Guid = employeeJobDtoUpdate.Guid,
            EmployeeGuid = employeeJobDtoUpdate.EmployeeGuid,
            InterviewGuid = employeeJobDtoUpdate.InterviewGuid,
            JobGuid = employeeJobDtoUpdate.JobGuid,
            StatusApproval = employeeJobDtoUpdate.StatusApproval,
            ModifiedDate = DateTime.UtcNow
        };
    }

    // explicit operator
    public static explicit operator EmployeeJobDtoUpdate(EmployeeJob employeeJob)
    {
        return new EmployeeJobDtoUpdate
        {
            Guid = employeeJob.Guid,
            EmployeeGuid = employeeJob.EmployeeGuid,
            InterviewGuid = employeeJob.InterviewGuid,
            JobGuid = employeeJob.JobGuid,
            StatusApproval = employeeJob.StatusApproval
        };
    }
}
