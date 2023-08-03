using Client.Contracts;
using Client.DataTransferObjects.EmployeeJobs;

namespace Client.Repositories;

public class EmployeeJobRepository : BaseRepository<EmployeeJobDtoGet, Guid>, IEmployeeJobRepository
{
    public EmployeeJobRepository(string request = "EmployeeJob/") : base(request)
    {
    }
}