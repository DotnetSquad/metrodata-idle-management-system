using Client.DataTransferObjects.EmployeeProjects;
using Client.Utilities.Handlers;

namespace Client.Contracts;

public interface IEmployeeProjectRepository : IBaseRepository<EmployeeProjectDtoGet, Guid>
{
    public Task<ResponseHandler<IEnumerable<EmployeeProjectDtoGet>>> GetByProject(Guid guid);
}
