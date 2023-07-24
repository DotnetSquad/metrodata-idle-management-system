using API.Models;

namespace API.Contracts;

public interface IEmployeeRepository : IBaseRepository<Employee>
{
    bool IsDuplicateValue(string value);
    Employee? GetEmployeeByEmail(string email);
}
