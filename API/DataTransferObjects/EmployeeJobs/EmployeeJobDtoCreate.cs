using API.Models;
using API.Utilities.Enums;

namespace API.DataTransferObjects.EmployeeJobs;

public class EmployeeJobDtoCreate
{
    public Guid EmployeeGuid { get; set; }
    public Guid? InterviewGuid { get; set; }
    public Guid JobGuid { get; set; }
    public StatusApprovalEnum StatusApproval { get; set; }

    // implicit operator
    public static implicit operator EmployeeJob(EmployeeJobDtoCreate employeeJobDtoCreate)
    {
        return new EmployeeJob
        {
            EmployeeGuid = employeeJobDtoCreate.EmployeeGuid,
            InterviewGuid = employeeJobDtoCreate.InterviewGuid,
            JobGuid = employeeJobDtoCreate.JobGuid,
            StatusApproval = employeeJobDtoCreate.StatusApproval,
            CreatedDate = DateTime.UtcNow
        };
    }

    // explicit operator
    public static explicit operator EmployeeJobDtoCreate(EmployeeJob employeeJob)
    {
        return new EmployeeJobDtoCreate
        {
            EmployeeGuid = employeeJob.EmployeeGuid,
            InterviewGuid = employeeJob.InterviewGuid,
            JobGuid = employeeJob.JobGuid,
            StatusApproval = employeeJob.StatusApproval
        };
    }
}
