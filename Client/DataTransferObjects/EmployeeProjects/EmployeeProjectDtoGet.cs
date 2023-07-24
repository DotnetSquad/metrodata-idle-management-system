namespace Client.DataTransferObjects.EmployeeProjects;

public class EmployeeProjectDtoGet
{
    public Guid Guid { get; set; }
    public Guid EmployeeGuid { get; set; }
    public Guid ProjectGuid { get; set; }
}
