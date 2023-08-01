using Client.Contracts;
using Client.DataTransferObjects.EmployeeInterviews;

namespace Client.Repositories;

public class EmployeeInterviewRepository : BaseRepository<EmployeeInterviewDtoGet, Guid>, IEmployeeInterviewRepository
{
    public EmployeeInterviewRepository(string request = "EmployeeInterview/") : base(request)
    {
    }
}