using API.Models;
using API.Utilities.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.DataTransferObjects.Grades;

public class GradeDtoGet
{
    public Guid Guid { get; set; }
    public GradeEnum GradeLevel { get; set; }
    public int ScoreSegment1 { get; set; }
    public int ScoreSegment2 { get; set; }
    public int ScoreSegment3 { get; set; }
    public int ScoreSegment4 { get; set; }
    public double TotalScore { get; set; }

    // implicit operator
    public static implicit operator Grade(GradeDtoGet gradeDtoGet)
    {
        return new Grade
        {
            Guid = gradeDtoGet.Guid,
            GradeLevel = gradeDtoGet.GradeLevel,
            ScoreSegment1 = gradeDtoGet.ScoreSegment1,
            ScoreSegment2 = gradeDtoGet.ScoreSegment2,
            ScoreSegment3 = gradeDtoGet.ScoreSegment3,
            ScoreSegment4 = gradeDtoGet.ScoreSegment4,
            TotalScore = gradeDtoGet.TotalScore
        };
    }

    // explicit operator
    public static explicit operator GradeDtoGet(Grade grade)
    {
        return new GradeDtoGet
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
