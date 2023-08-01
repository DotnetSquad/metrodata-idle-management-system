using Client.DataTransferObjects.Employees;
using Client.Utilities.Handlers;

namespace Client.Contracts;

public interface IEmployeeRepository : IBaseRepository<EmployeeDtoGet, Guid>
{
    public Task<ResponseHandler<IEnumerable<EmployeeDtoGet>>> GetByRole(Guid guid);
    public Task<ResponseHandler<IEnumerable<EmployeeDtoGet>>> GetExcludeRole(Guid guid);
}
