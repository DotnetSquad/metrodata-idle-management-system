namespace Client.DataTransferObjects.EmployeeProjects;

public class EmployeeProjectDtoUpdate
{
    public Guid Guid { get; set; }
    public Guid EmployeeGuid { get; set; }
    public Guid ProjectGuid { get; set; }
}
