using API.Utilities.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

[Table("tb_m_grades")]
public class Grade : BaseEntity
{
    [Column("grade_level")]
    public GradeEnum GradeLevel { get; set; }

    [Column("score_segment1")]
    public int ScoreSegment1 { get; set; }

    [Column("score_segment2")]
    public int ScoreSegment2 { get; set; }

    [Column("score_segment3")]
    public int ScoreSegment3 { get; set; }

    [Column("score_segment4")]
    public int ScoreSegment4 { get; set; }

    [Column("total_score")]
    public double TotalScore { get; set; }

    // Cardinality
    public Employee? Employee { get; set; }
}
