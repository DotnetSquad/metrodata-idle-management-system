using Client.Contracts;
using Client.DataTransferObjects.Grades;

namespace Client.Repositories;

public class GradeRepository : BaseRepository<GradeDtoGet, Guid>, IGradeRepository
{
    public GradeRepository(string request = "Grade/") : base(request)
    {
    }
}
