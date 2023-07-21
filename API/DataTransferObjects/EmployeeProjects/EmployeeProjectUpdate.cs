namespace API.DataTransferObjects.EmployeeProjects;

public class EmployeeProjectUpdate
{
    public Guid Guid { get; set; }
    public Guid EmployeeGuid { get; set; }
    public Guid ProjectGuid { get; set; }
}
