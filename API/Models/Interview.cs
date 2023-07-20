using API.Utilities.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

[Table("tb_m_interviews")]
public class Interview : BaseEntity
{
    [Column("job_name", TypeName = "nvarchar(255)")]
    public string JobName { get; set; }

    [Column("link", TypeName = "nvarchar(255)")]
    public string? Link { get; set; }

    [Column("interview_date")]
    public DateTime InterviewDate { get; set; }

    [Column("description", TypeName = "nvarchar(255)")]
    public string Description { get; set; }

    [Column("status")]
    public StatusInterviewEnum Status { get; set; }

    [Column("job_guid")]
    public Guid JobGuid { get; set; }

    // Cardinality
    /*public Job? Job { get; set; }
    public ICollection<EmployeeInterview>? EmployeeInterviews { get; set; }*/
}
