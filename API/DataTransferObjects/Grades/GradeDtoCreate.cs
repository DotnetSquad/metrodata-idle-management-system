using API.Models;
using API.Utilities.Enums;

namespace API.DataTransferObjects.Grades;

public class GradeDtoCreate
{
    public GradeEnum GradeLevel { get; set; }
    public int ScoreSegment1 { get; set; }
    public int ScoreSegment2 { get; set; }
    public int ScoreSegment3 { get; set; }
    public int ScoreSegment4 { get; set; }
    public double TotalScore { get; set; }

    // implicit operator
    public static implicit operator Grade(GradeDtoCreate gradeDtoCreate)
    {
        return new Grade
        {
            GradeLevel = gradeDtoCreate.GradeLevel,
            ScoreSegment1 = gradeDtoCreate.ScoreSegment1,
            ScoreSegment2 = gradeDtoCreate.ScoreSegment2,
            ScoreSegment3 = gradeDtoCreate.ScoreSegment3,
            ScoreSegment4 = gradeDtoCreate.ScoreSegment4,
            TotalScore = gradeDtoCreate.TotalScore
        };
    }

    // explicit operator
    public static explicit operator GradeDtoCreate(Grade grade)
    {
        return new GradeDtoCreate
        {
            GradeLevel = grade.GradeLevel,
            ScoreSegment1 = grade.ScoreSegment1,
            ScoreSegment2 = grade.ScoreSegment2,
            ScoreSegment3 = grade.ScoreSegment3,
            ScoreSegment4 = grade.ScoreSegment4,
            TotalScore = grade.TotalScore
        };
    }
}
