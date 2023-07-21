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
}
