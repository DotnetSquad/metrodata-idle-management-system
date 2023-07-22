using API.Models;
using API.Utilities.Enums;

namespace API.DataTransferObjects.Grades;

public class GradeDtoUpdate
{
    public Guid Guid { get; set; }
    public GradeEnum GradeLevel { get; set; }
    public int ScoreSegment1 { get; set; }
    public int ScoreSegment2 { get; set; }
    public int ScoreSegment3 { get; set; }
    public int ScoreSegment4 { get; set; }
    public double TotalScore { get; set; }

    // implicit operator
    public static implicit operator Grade(GradeDtoUpdate gradeDtoUpdate)
    {
        return new Grade
        {
            Guid = gradeDtoUpdate.Guid,
            GradeLevel = gradeDtoUpdate.GradeLevel,
            ScoreSegment1 = gradeDtoUpdate.ScoreSegment1,
            ScoreSegment2 = gradeDtoUpdate.ScoreSegment2,
            ScoreSegment3 = gradeDtoUpdate.ScoreSegment3,
            ScoreSegment4 = gradeDtoUpdate.ScoreSegment4,
            TotalScore = gradeDtoUpdate.TotalScore,
            ModifiedDate = DateTime.UtcNow
        };
    }

    // explicit operator
    public static explicit operator GradeDtoUpdate(Grade grade)
    {
        return new GradeDtoUpdate
        {
            Guid = grade.Guid,
            GradeLevel = grade.GradeLevel,
            ScoreSegment1 = grade.ScoreSegment1,
            ScoreSegment2 = grade.ScoreSegment2,
            ScoreSegment3 = grade.ScoreSegment3,
            ScoreSegment4 = grade.ScoreSegment4,
            TotalScore = grade.TotalScore
        };
    }
}
