﻿namespace Client.DataTransferObjects.Projects;

public class ProjectDtoUpdate
{
    public Guid Guid { get; set; }
    public string NameProject { get; set; }
    public string ProjectLead { get; set; }
    public string Description { get; set; }
}
