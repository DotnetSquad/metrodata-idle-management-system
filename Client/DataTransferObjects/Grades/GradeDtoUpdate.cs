using Client.Utilities.Enums;

namespace Client.DataTransferObjects.Grades;

public class GradeDtoUpdate
{
    public Guid Guid { get; set; }
    public GradeEnum GradeLevel { get; set; }
    public int ScoreSegment1 { get; set; }
    public int ScoreSegment2 { get; set; }
    public int ScoreSegment3 { get; set; }
    public int ScoreSegment4 { get; set; }
    public double TotalScore { get; set; }
}
