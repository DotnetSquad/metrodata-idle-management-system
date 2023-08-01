using Client.DataTransferObjects.EmployeeProjects;

namespace Client.Contracts;

public interface IEmployeeProjectRepository : IBaseRepository<EmployeeProjectDtoGet, Guid>
{
}
