using Client.DataTransferObjects.Grades;
using Client.Utilities.Handlers;

namespace Client.Contracts;

public interface IGradeRepository : IBaseRepository<GradeDtoGet, Guid>
{
    Task<ResponseHandler<GradeDtoGenerateScore>> PostGenerate(GradeDtoGenerateScore entity);
    Task<ResponseHandler<GradeDtoGet>> PutGenerate(Guid id, GradeDtoGet entity);
}
