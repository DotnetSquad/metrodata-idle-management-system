using Client.DataTransferObjects.EmployeeJobs;

namespace Client.Contracts;

public interface IEmployeeJobRepository : IBaseRepository<EmployeeJobDtoGet, Guid>
{
}
