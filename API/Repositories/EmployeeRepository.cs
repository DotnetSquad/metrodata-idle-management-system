using API.Contracts;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

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
}
