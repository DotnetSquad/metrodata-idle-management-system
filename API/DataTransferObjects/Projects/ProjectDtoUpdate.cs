using API.Models;

namespace API.DataTransferObjects.Projects;

public class ProjectDtoUpdate
{
    public Guid Guid { get; set; }
    public string NameProject { get; set; }
    public string ProjectLead { get; set; }
    public string Description { get; set; }

    // implicit operator
    public static implicit operator Project(ProjectDtoUpdate projectDtoUpdate)
    {
        return new Project
        {
            Guid = projectDtoUpdate.Guid,
            NameProject = projectDtoUpdate.NameProject,
            ProjectLead = projectDtoUpdate.ProjectLead,
            Description = projectDtoUpdate.Description
        };
    }

    // explicit operator
    public static explicit operator ProjectDtoUpdate(Project project)
    {
        return new ProjectDtoUpdate
        {
            Guid = project.Guid,
            NameProject = project.NameProject,
            ProjectLead = project.ProjectLead,
            Description = project.Description
        };
    }
}
