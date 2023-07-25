using Client.Contracts;
using Client.DataTransferObjects.Projects;

namespace Client.Repositories;

public class ProjectRepository : BaseRepository<ProjectDtoGet, Guid>, IProjectRepository
{
    public ProjectRepository(string request = "Project/") : base(request)
    {
    }
}
