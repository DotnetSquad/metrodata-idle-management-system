using API.Models;

namespace API.DataTransferObjects.Grades;

public class GradeDtoUpdateGenerateScore
{
    public Guid Guid { get; set; }
    public int ScoreSegment1 { get; set; }
    public int ScoreSegment2 { get; set; }
    public int ScoreSegment3 { get; set; }
    public int ScoreSegment4 { get; set; }

    // implicit operator
    public static implicit operator Grade(GradeDtoUpdateGenerateScore gradeDtoUpdateGenerateScore)
    {
        return new Grade
        {
            Guid = gradeDtoUpdateGenerateScore.Guid,
            ScoreSegment1 = gradeDtoUpdateGenerateScore.ScoreSegment1,
            ScoreSegment2 = gradeDtoUpdateGenerateScore.ScoreSegment2,
            ScoreSegment3 = gradeDtoUpdateGenerateScore.ScoreSegment3,
            ScoreSegment4 = gradeDtoUpdateGenerateScore.ScoreSegment4,
            ModifiedDate = DateTime.UtcNow
        };
    }

    // explicit operator
    public static explicit operator GradeDtoUpdateGenerateScore(Grade grade)
    {
        return new GradeDtoUpdateGenerateScore
        {
            Guid = grade.Guid,
            ScoreSegment1 = grade.ScoreSegment1,
            ScoreSegment2 = grade.ScoreSegment2,
            ScoreSegment3 = grade.ScoreSegment3,
            ScoreSegment4 = grade.ScoreSegment4
        };
    }
}
