using API.Models;

namespace API.DataTransferObjects.EmployeeProjects;

public class EmployeeProjectCreate
{
    public Guid EmployeeGuid { get; set; }
    public Guid ProjectGuid { get; set; }

    // implicit operator
    public static implicit operator EmployeeProject(EmployeeProjectCreate employeeProjectCreate)
    {
        return new EmployeeProject
        {
            EmployeeGuid = employeeProjectCreate.EmployeeGuid,
            ProjectGuid = employeeProjectCreate.ProjectGuid
        };
    }

    // explicit operator
    public static explicit operator EmployeeProjectCreate(EmployeeProject employeeProject)
    {
        return new EmployeeProjectCreate
        {
            EmployeeGuid = employeeProject.EmployeeGuid,
            ProjectGuid = employeeProject.ProjectGuid
        };
    }
}
