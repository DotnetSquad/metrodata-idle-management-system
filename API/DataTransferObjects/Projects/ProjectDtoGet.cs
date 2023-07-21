using API.Models;

namespace API.DataTransferObjects.Projects;

public class ProjectDtoGet
{
    public Guid Guid { get; set; }
    public string NameProject { get; set; }
    public string ProjectLead { get; set; }
    public string Description { get; set; }

    // implicit operator
    public static implicit operator Project(ProjectDtoGet projectDtoGet)
    {
        return new Project
        {
            Guid = projectDtoGet.Guid,
            NameProject = projectDtoGet.NameProject,
            ProjectLead = projectDtoGet.ProjectLead,
            Description = projectDtoGet.Description
        };
    }

    // explicit operator
    public static explicit operator ProjectDtoGet(Project project)
    {
        return new ProjectDtoGet
        {
            Guid = project.Guid,
            NameProject = project.NameProject,
            ProjectLead = project.ProjectLead,
            Description = project.Description
        };
    }
}
