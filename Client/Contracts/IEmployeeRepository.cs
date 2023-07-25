using Client.DataTransferObjects.Employees;

namespace Client.Contracts;

public interface IEmployeeRepository : IBaseRepository<EmployeeDtoGet, Guid>
{
}