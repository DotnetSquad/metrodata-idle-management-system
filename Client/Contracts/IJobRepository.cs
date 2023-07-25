using Client.DataTransferObjects.Jobs;

namespace Client.Contracts;

public interface IJobRepository : IBaseRepository<JobDtoGet, Guid>
{
}
