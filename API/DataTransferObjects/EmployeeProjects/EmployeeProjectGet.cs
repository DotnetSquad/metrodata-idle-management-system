namespace API.DataTransferObjects.EmployeeProjects;

public class EmployeeProjectGet
{
    public Guid Guid { get; set; }
    public Guid EmployeeGuid { get; set; }
    public Guid ProjectGuid { get; set; }
}
