using Client.DataTransferObjects.EmployeeJobs;
using Client.Utilities.Handlers;

namespace Client.Contracts;

public interface IEmployeeJobRepository : IBaseRepository<EmployeeJobDtoGet, Guid>
{
    Task<ResponseHandler<IEnumerable<EmployeeJobDtoGet>>> GetByJob(Guid guid);
}
