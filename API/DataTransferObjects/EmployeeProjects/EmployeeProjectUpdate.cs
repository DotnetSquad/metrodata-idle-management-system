using API.Models;

namespace API.DataTransferObjects.EmployeeProjects;

public class EmployeeProjectUpdate
{
    public Guid Guid { get; set; }
    public Guid EmployeeGuid { get; set; }
    public Guid ProjectGuid { get; set; }
    
    // implicit operator
    public static implicit operator EmployeeProject(EmployeeProjectUpdate employeeProjectUpdate)
    {
        return new EmployeeProject()
        {
            Guid = employeeProjectUpdate.Guid,
            EmployeeGuid = employeeProjectUpdate.EmployeeGuid,
            ProjectGuid = employeeProjectUpdate.ProjectGuid
        };
    }
    
    // explicit operator
    public static explicit operator EmployeeProjectUpdate(EmployeeProject employeeProject)
    {
        return new EmployeeProjectUpdate()
        {
            Guid = employeeProject.Guid,
            EmployeeGuid = employeeProject.EmployeeGuid,
            ProjectGuid = employeeProject.ProjectGuid
        };
    }
}
