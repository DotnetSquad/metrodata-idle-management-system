using Client.Contracts;
using Client.DataTransferObjects.Employees;

namespace Client.Repositories;

public class EmployeeRepository : BaseRepository<EmployeeDtoGet, Guid>, IEmployeeRepository
{
    public EmployeeRepository(string request = "Employee/") : base(request)
    {
    }
}
