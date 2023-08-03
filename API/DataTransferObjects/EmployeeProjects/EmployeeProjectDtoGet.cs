using API.Models;
using API.Utilities.Enums;

namespace API.DataTransferObjects.EmployeeProjects;

public class EmployeeProjectDtoGet
{
    public Guid Guid { get; set; }
    public Guid EmployeeGuid { get; set; }
    public Guid ProjectGuid { get; set; }
    public StatusApprovalEnum StatusApproval { get; set; }

    // implicit operator
    public static implicit operator EmployeeProject(EmployeeProjectDtoGet employeeProjectDtoGet)
    {
        return new EmployeeProject
        {
            Guid = employeeProjectDtoGet.Guid,
            EmployeeGuid = employeeProjectDtoGet.EmployeeGuid,
            ProjectGuid = employeeProjectDtoGet.ProjectGuid,
            StatusApproval = employeeProjectDtoGet.StatusApproval
        };
    }

    // explicit operator
    public static explicit operator EmployeeProjectDtoGet(EmployeeProject employeeProject)
    {
        return new EmployeeProjectDtoGet
        {
            Guid = employeeProject.Guid,
            EmployeeGuid = employeeProject.EmployeeGuid,
            ProjectGuid = employeeProject.ProjectGuid,
            StatusApproval = employeeProject.StatusApproval
        };
    }
}
