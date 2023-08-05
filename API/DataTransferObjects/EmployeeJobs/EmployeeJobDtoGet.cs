using API.Models;
using API.Utilities.Enums;

namespace API.DataTransferObjects.EmployeeJobs;

public class EmployeeJobDtoGet
{
    public Guid Guid { get; set; }
    public Guid EmployeeGuid { get; set; }
    public Guid InterviewGuid { get; set; }
    public Guid JobGuid { get; set; }
    public StatusApprovalEnum StatusApproval { get; set; }

    // implicit operator
    public static implicit operator EmployeeJob(EmployeeJobDtoGet employeeJobDtoGet)
    {
        return new EmployeeJob
        {
            Guid = employeeJobDtoGet.Guid,
            EmployeeGuid = employeeJobDtoGet.EmployeeGuid,
            InterviewGuid = employeeJobDtoGet.InterviewGuid,
            JobGuid = employeeJobDtoGet.JobGuid,
            StatusApproval = employeeJobDtoGet.StatusApproval
        };
    }

    // explicit operator
    public static explicit operator EmployeeJobDtoGet(EmployeeJob employeeJob)
    {
        return new EmployeeJobDtoGet
        {
            Guid = employeeJob.Guid,
            EmployeeGuid = employeeJob.EmployeeGuid,
            InterviewGuid = employeeJob.InterviewGuid,
            JobGuid = employeeJob.JobGuid,
            StatusApproval = employeeJob.StatusApproval
        };
    }
}
