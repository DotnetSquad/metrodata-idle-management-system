using Client.Contracts;
using Client.DataTransferObjects.Jobs;

namespace Client.Repositories;

public class JobRepository : BaseRepository<JobDtoGet, Guid>, IJobRepository
{
    public JobRepository(string request = "Job/") : base(request)
    {
    }
}
