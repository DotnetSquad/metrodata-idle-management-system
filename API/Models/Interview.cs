using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

[Table("tb_m_interviews")]
public class Interview : BaseEntity
{
    [Column("title", TypeName = "nvarchar(255)")]
    public string Title { get; set; }

    [Column("link", TypeName = "nvarchar(255)")]
    public string? Link { get; set; }

    [Column("interview_date")]
    public DateTime InterviewDate { get; set; }

    [Column("description", TypeName = "nvarchar(255)")]
    public string Description { get; set; }

    // Cardinality
    public EmployeeJob? EmployeeJob { get; set; }
}
