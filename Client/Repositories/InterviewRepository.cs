using Client.Contracts;
using Client.DataTransferObjects.Interviews;

namespace Client.Repositories;

public class InterviewRepository : BaseRepository<InterviewDtoGet, Guid>, IInterviewRepository
{
    public InterviewRepository(string request = "Interview/") : base(request)
    {
    }
}
