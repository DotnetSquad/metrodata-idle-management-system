using API.Models;

namespace API.DataTransferObjects.EmployeeProjects;

public class EmployeeProjectDtoCreate
{
    public Guid EmployeeGuid { get; set; }
    public Guid ProjectGuid { get; set; }

    // implicit operator
    public static implicit operator EmployeeProject(EmployeeProjectDtoCreate employeeProjectDtoCreate)
    {
        return new EmployeeProject
        {
            EmployeeGuid = employeeProjectDtoCreate.EmployeeGuid,
            ProjectGuid = employeeProjectDtoCreate.ProjectGuid,
            CreatedDate = DateTime.UtcNow
        };
    }

    // explicit operator
    public static explicit operator EmployeeProjectDtoCreate(EmployeeProject employeeProject)
    {
        return new EmployeeProjectDtoCreate
        {
            EmployeeGuid = employeeProject.EmployeeGuid,
            ProjectGuid = employeeProject.ProjectGuid
        };
    }
}
