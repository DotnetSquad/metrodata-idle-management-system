using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

public class EmployeeInterviewRepository : BaseRepository<EmployeeInterview>, IEmployeeInterviewRepository
{
    public EmployeeInterviewRepository(ApplicationDbContext context) : base(context)
    {
    }
}
