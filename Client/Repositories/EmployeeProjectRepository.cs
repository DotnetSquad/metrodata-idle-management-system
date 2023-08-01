using Client.Contracts;
using Client.DataTransferObjects.EmployeeProjects;

namespace Client.Repositories;

public class EmployeeProjectRepository : BaseRepository<EmployeeProjectDtoGet, Guid>, IEmployeeProjectRepository
{
    public EmployeeProjectRepository(string request = "EmployeeProject/") : base(request)
    {
    }
}
