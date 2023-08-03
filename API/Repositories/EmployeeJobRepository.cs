using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

public class EmployeeJobRepository : BaseRepository<EmployeeJob>, IEmployeeJobRepository
{
    public EmployeeJobRepository(ApplicationDbContext context) : base(context)
    {
    }
}
