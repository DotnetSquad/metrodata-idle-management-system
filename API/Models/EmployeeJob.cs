using API.Utilities.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

[Table("tb_tr_employee_job")]
public class EmployeeJob : BaseEntity
{
    [Column("employee_guid")]
    public Guid EmployeeGuid { get; set; }

    [Column("interview_guid")]
    public Guid? InterviewGuid { get; set; }

    [Column("job_guid")]
    public Guid JobGuid { get; set; }

    [Column("status_approval")]
    public StatusApprovalEnum StatusApproval { get; set; }

    // Cardinality
    public Employee? Employee { get; set; }
    public Interview? Interview { get; set; }
    public Job? Job { get; set; }
}
