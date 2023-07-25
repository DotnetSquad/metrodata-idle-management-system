using Client.DataTransferObjects.Projects;

namespace Client.Contracts;

public interface IProjectRepository : IBaseRepository<ProjectDtoGet, Guid>
{
    
}
