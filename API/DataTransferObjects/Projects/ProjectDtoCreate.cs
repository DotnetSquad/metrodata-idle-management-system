using API.Models;

namespace API.DataTransferObjects.Projects;

public class ProjectDtoCreate
{
    public string NameProject { get; set; }
    public string ProjectLead { get; set; }
    public string Description { get; set; }

    // implicit operator
    public static implicit operator Project(ProjectDtoCreate projectDtoCreate)
    {
        return new Project
        {
            NameProject = projectDtoCreate.NameProject,
            ProjectLead = projectDtoCreate.ProjectLead,
            Description = projectDtoCreate.Description
        };
    }

    // explicit operator
    public static explicit operator ProjectDtoCreate(Project project)
    {
        return new ProjectDtoCreate
        {
            NameProject = project.NameProject,
            ProjectLead = project.ProjectLead,
            Description = project.Description
        };
    }
}
