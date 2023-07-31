using API.Contracts;
using API.Data;
using API.Models;
using API.Utilities.Enums;

namespace API.Repositories;

public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(ApplicationDbContext context) : base(context)
    {
    }

    public bool IsDuplicateValue(string value)
    {
        return Context.Set<Employee>().FirstOrDefault(e => e.Email.Contains(value) || e.PhoneNumber.Contains(value)) is null;
    }

    public Employee? GetByEmail(string email)
    {
        return Context.Set<Employee>().FirstOrDefault(e => e.Email == email);
    }

    public string? GetLastEmployeeNik()
    {
        return Context.Set<Employee>().ToList().Select(e => e.Nik).LastOrDefault();
    }

    public int GetIdleEmployeeStatus()
    {
        return Context.Set<Employee>().Count(a => a.Status == StatusEnum.Idle);
    }

    public int GetWorkingEmployeeStatus()
    {
        return Context.Set<Employee>().Count(a => a.Status == StatusEnum.Working);
    }
}
