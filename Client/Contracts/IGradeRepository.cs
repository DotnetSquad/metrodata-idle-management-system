using Client.DataTransferObjects.Grades;

namespace Client.Contracts;

public interface IGradeRepository : IBaseRepository<GradeDtoGet, Guid>
{
}
