using API.Models;
using API.Utilities.Enums;

namespace API.DataTransferObjects.EmployeeInterviews;

public class EmployeeInterviewDtoGet
{
    public Guid Guid { get; set; }
    public Guid EmployeeGuid { get; set; }
    public Guid InterviewGuid { get; set; }
    public StatusApprovalEnum StatusApproval { get; set; }

    // implicit operator
    public static implicit operator EmployeeInterview(EmployeeInterviewDtoGet employeeInterviewDtoGet)
    {
        return new EmployeeInterview
        {
            Guid = employeeInterviewDtoGet.Guid,
            EmployeeGuid = employeeInterviewDtoGet.EmployeeGuid,
            InterviewGuid = employeeInterviewDtoGet.InterviewGuid,
            StatusApproval = employeeInterviewDtoGet.StatusApproval
        };
    }

    // explicit operator
    public static explicit operator EmployeeInterviewDtoGet(EmployeeInterview employeeInterview)
    {
        return new EmployeeInterviewDtoGet
        {
            Guid = employeeInterview.Guid,
            EmployeeGuid = employeeInterview.EmployeeGuid,
            InterviewGuid = employeeInterview.InterviewGuid,
            StatusApproval = employeeInterview.StatusApproval
        };
    }
}
