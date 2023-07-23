using API.Contracts;
using API.DataTransferObjects.Grades;

namespace API.Services;

public class GradeService
{
    private IGradeRepository _gradeRepository;

    public GradeService(IGradeRepository gradeRepository)
    {
        _gradeRepository = gradeRepository;
    }

    public IEnumerable<GradeDtoGet> Get()
    {
        var grades = _gradeRepository.GetAll().ToList();
        if (!grades.Any()) return Enumerable.Empty<GradeDtoGet>();
        List<GradeDtoGet> gradeDtoGets = new();

        foreach (var grade in grades)
        {
            gradeDtoGets.Add((GradeDtoGet)grade);
        }

        return gradeDtoGets;
    }

    public GradeDtoGet? Get(Guid guid)
    {
        var grade = _gradeRepository.GetByGuid(guid);
        if (grade is null) return null!;

        return (GradeDtoGet)grade;
    }

    public GradeDtoCreate? Create(GradeDtoCreate gradeDtoCreate)
    {
        var gradeCreated = _gradeRepository.Create(gradeDtoCreate);
        if (gradeCreated is null) return null!;

        return (GradeDtoCreate)gradeCreated;
    }

    public int Update(GradeDtoUpdate gradeDtoUpdate)
    {
        var grade = _gradeRepository.GetByGuid(gradeDtoUpdate.Guid);
        if (grade is null) return -1;

        var gradeUpdated = _gradeRepository.Update(gradeDtoUpdate);
        return !gradeUpdated ? 0 : 1;
    }

    public int Delete(Guid guid)
    {
        var grade = _gradeRepository.GetByGuid(guid);
        if (grade is null) return -1;

        var gradeDeleted = _gradeRepository.Delete(grade);
        return !gradeDeleted ? 0 : 1;
    }
}
