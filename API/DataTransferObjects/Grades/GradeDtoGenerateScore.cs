using API.Models;

namespace API.DataTransferObjects.Grades;

public class GradeDtoGenerateScore
{
    public int ScoreSegment1 { get; set; }
    public int ScoreSegment2 { get; set; }
    public int ScoreSegment3 { get; set; }
    public int ScoreSegment4 { get; set; }

    // implicit operator
    public static implicit operator Grade(GradeDtoGenerateScore gradeDtoGenerateScore)
    {
        return new Grade
        {
            ScoreSegment1 = gradeDtoGenerateScore.ScoreSegment1,
            ScoreSegment2 = gradeDtoGenerateScore.ScoreSegment2,
            ScoreSegment3 = gradeDtoGenerateScore.ScoreSegment3,
            ScoreSegment4 = gradeDtoGenerateScore.ScoreSegment4,
            CreatedDate = DateTime.UtcNow
        };
    }

    // explicit operator
    public static explicit operator GradeDtoGenerateScore(Grade grade)
    {
        return new GradeDtoGenerateScore
        {
            ScoreSegment1 = grade.ScoreSegment1,
            ScoreSegment2 = grade.ScoreSegment2,
            ScoreSegment3 = grade.ScoreSegment3,
            ScoreSegment4 = grade.ScoreSegment4
        };
    }
}
