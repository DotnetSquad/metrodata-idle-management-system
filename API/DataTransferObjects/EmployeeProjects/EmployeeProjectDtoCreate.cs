using API.Models;
using API.Utilities.Enums;

namespace API.DataTransferObjects.EmployeeProjects;

public class EmployeeProjectDtoCreate
{
    public Guid EmployeeGuid { get; set; }
    public Guid ProjectGuid { get; set; }
    public StatusApprovalEnum StatusApproval { get; set; }

    // implicit operator
    public static implicit operator EmployeeProject(EmployeeProjectDtoCreate employeeProjectDtoCreate)
    {
        return new EmployeeProject
        {
            EmployeeGuid = employeeProjectDtoCreate.EmployeeGuid,
            ProjectGuid = employeeProjectDtoCreate.ProjectGuid,
            StatusApproval = employeeProjectDtoCreate.StatusApproval,
            CreatedDate = DateTime.UtcNow
        };
    }

    // explicit operator
    public static explicit operator EmployeeProjectDtoCreate(EmployeeProject employeeProject)
    {
        return new EmployeeProjectDtoCreate
        {
            EmployeeGuid = employeeProject.EmployeeGuid,
            ProjectGuid = employeeProject.ProjectGuid,
            StatusApproval = employeeProject.StatusApproval
        };
    }
}
