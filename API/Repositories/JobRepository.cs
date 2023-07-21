using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

public class JobRepository : BaseRepository<Job>, IJobInterface
{
    public JobRepository(ApplicationDbContext context) : base(context)
    {
    }
}
