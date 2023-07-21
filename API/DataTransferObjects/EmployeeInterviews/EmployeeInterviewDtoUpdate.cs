using API.Models;

namespace API.DataTransferObjects.EmployeeInterviews;

public class EmployeeInterviewDtoUpdate
{
    public Guid Guid { get; set; }
    public Guid EmployeeGuid { get; set; }
    public Guid InterviewGuid { get; set; }

    // implicit operator
    public static implicit operator EmployeeInterview(EmployeeInterviewDtoUpdate employeeInterviewDtoUpdate)
    {
        return new EmployeeInterview
        {
            Guid = employeeInterviewDtoUpdate.Guid,
            EmployeeGuid = employeeInterviewDtoUpdate.EmployeeGuid,
            InterviewGuid = employeeInterviewDtoUpdate.InterviewGuid
        };
    }

    // explicit operator
    public static explicit operator EmployeeInterviewDtoUpdate(EmployeeInterview employeeInterview)
    {
        return new EmployeeInterviewDtoUpdate
        {
            Guid = employeeInterview.Guid,
            EmployeeGuid = employeeInterview.EmployeeGuid,
            InterviewGuid = employeeInterview.InterviewGuid
        };
    }
}
