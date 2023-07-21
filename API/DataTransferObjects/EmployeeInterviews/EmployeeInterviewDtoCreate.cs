using API.Models;

namespace API.DataTransferObjects.EmployeeInterviews;

public class EmployeeInterviewDtoCreate
{
    public Guid EmployeeGuid { get; set; }
    public Guid InterviewGuid { get; set; }

    // implicit operator
    public static implicit operator EmployeeInterview(EmployeeInterviewDtoCreate employeeInterviewDtoCreate)
    {
        return new EmployeeInterview
        {
            EmployeeGuid = employeeInterviewDtoCreate.EmployeeGuid,
            InterviewGuid = employeeInterviewDtoCreate.InterviewGuid
        };
    }

    // explicit operator
    public static explicit operator EmployeeInterviewDtoCreate(EmployeeInterview employeeInterview)
    {
        return new EmployeeInterviewDtoCreate
        {
            EmployeeGuid = employeeInterview.EmployeeGuid,
            InterviewGuid = employeeInterview.InterviewGuid
        };
    }
}
