using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

public class EmployeeProjectRepository : BaseRepository<EmployeeProject>, IEmployeeProjectRepository
{
    public EmployeeProjectRepository(ApplicationDbContext Context) : base(Context) { }
}
