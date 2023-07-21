using API.Models;

namespace API.DataTransferObjects.EmployeeProjects;

public class EmployeeProjectGet
{
    public Guid Guid { get; set; }
    public Guid EmployeeGuid { get; set; }
    public Guid ProjectGuid { get; set; }
    
    // implicit operator
    public static implicit operator EmployeeProject(EmployeeProjectGet employeeProjectGet)
    {
        return new EmployeeProject
        {
            Guid = employeeProjectGet.Guid,
            EmployeeGuid = employeeProjectGet.EmployeeGuid,
            ProjectGuid = employeeProjectGet.ProjectGuid
        };
    }
    
    // explicit operator
    public static explicit operator EmployeeProjectGet(EmployeeProject employeeProject)
    {
        return new EmployeeProjectGet
        {
            Guid = employeeProject.Guid,
            EmployeeGuid = employeeProject.EmployeeGuid,
            ProjectGuid = employeeProject.ProjectGuid
        };
    }
}
