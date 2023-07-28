using API.Contracts;
using API.Data;
using API.DataTransferObjects.Grades;
using API.Models;
using API.Utilities.Handlers;

namespace API.Services;

public class GradeService
{
    private readonly ApplicationDbContext _context;
    private IGradeRepository _gradeRepository;

    public GradeService(IGradeRepository gradeRepository, ApplicationDbContext context)
    {
        _gradeRepository = gradeRepository;
        _context = context;
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

    public GradeDtoCreate? CreateGenerateScore(GradeDtoGenerateScore gradeDtoGenerateScore)
    {
        using var transaction = _context.Database.BeginTransaction();
        try
        {
            Grade gradeData = new GradeDtoCreate
            {
                ScoreSegment1 = gradeDtoGenerateScore.ScoreSegment1,
                ScoreSegment2 = gradeDtoGenerateScore.ScoreSegment2,
                ScoreSegment3 = gradeDtoGenerateScore.ScoreSegment3,
                ScoreSegment4 = gradeDtoGenerateScore.ScoreSegment4,
            };
            gradeData.TotalScore = GenerateHandler.GenerateTotalScore(gradeData.ScoreSegment1, gradeData.ScoreSegment2, gradeData.ScoreSegment3, gradeData.ScoreSegment4);
            gradeData.GradeLevel = GenerateHandler.GenerateGradeLevel(gradeData.ScoreSegment1, gradeData.ScoreSegment2, gradeData.ScoreSegment3, gradeData.ScoreSegment4);

            var gradeCreated = _gradeRepository.Create(gradeData);
            if (gradeCreated is null) return null!;

            transaction.Commit();
            return (GradeDtoCreate)gradeCreated;
        }
        catch
        {
            transaction.Rollback();
            return null;
        }
    }

    public int UpdateGenerateScore(GradeDtoUpdateGenerateScore gradeDtoUpdateGenerateScore)
    {
        using var transaction = _context.Database.BeginTransaction();
        try
        {
            var grade = _gradeRepository.GetByGuid(gradeDtoUpdateGenerateScore.Guid);
            if (grade is null) return -1;

            Grade gradeData = new GradeDtoUpdate
            {
                Guid = gradeDtoUpdateGenerateScore.Guid,
                ScoreSegment1 = gradeDtoUpdateGenerateScore.ScoreSegment1,
                ScoreSegment2 = gradeDtoUpdateGenerateScore.ScoreSegment2,
                ScoreSegment3 = gradeDtoUpdateGenerateScore.ScoreSegment3,
                ScoreSegment4 = gradeDtoUpdateGenerateScore.ScoreSegment4,
            };
            gradeData.TotalScore = GenerateHandler.GenerateTotalScore(gradeData.ScoreSegment1, gradeData.ScoreSegment2, gradeData.ScoreSegment3, gradeData.ScoreSegment4);
            gradeData.GradeLevel = GenerateHandler.GenerateGradeLevel(gradeData.ScoreSegment1, gradeData.ScoreSegment2, gradeData.ScoreSegment3, gradeData.ScoreSegment4);

            var gradeUpdated = _gradeRepository.Update(gradeData);
            if (!gradeUpdated) return 0;

            transaction.Commit();
            return 1;
        }
        catch
        {
            transaction.Rollback();
            return 0;
        }
    }
}
