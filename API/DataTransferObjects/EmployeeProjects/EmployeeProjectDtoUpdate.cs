using API.Models;

namespace API.DataTransferObjects.EmployeeProjects;

public class EmployeeProjectDtoUpdate
{
    public Guid Guid { get; set; }
    public Guid EmployeeGuid { get; set; }
    public Guid ProjectGuid { get; set; }

    // implicit operator
    public static implicit operator EmployeeProject(EmployeeProjectDtoUpdate employeeProjectDtoUpdate)
    {
        return new EmployeeProject
        {
            Guid = employeeProjectDtoUpdate.Guid,
            EmployeeGuid = employeeProjectDtoUpdate.EmployeeGuid,
            ProjectGuid = employeeProjectDtoUpdate.ProjectGuid
        };
    }

    // explicit operator
    public static explicit operator EmployeeProjectDtoUpdate(EmployeeProject employeeProject)
    {
        return new EmployeeProjectDtoUpdate
        {
            Guid = employeeProject.Guid,
            EmployeeGuid = employeeProject.EmployeeGuid,
            ProjectGuid = employeeProject.ProjectGuid
        };
    }
}
