using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

public class InterviewRepository : BaseRepository<Interview>, IInterviewRepository
{
    public InterviewRepository(ApplicationDbContext context) : base(context)
    {
    }
}
