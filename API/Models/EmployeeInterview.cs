using System.ComponentModel.DataAnnotations.Schema;
using API.Utilities.Enums;

namespace API.Models;

[Table("tb_tr_employee_interview")]
public class EmployeeInterview : BaseEntity
{
    [Column("employee_guid")]
    public Guid EmployeeGuid { get; set; }

    [Column("interview_guid")]
    public Guid InterviewGuid { get; set; }
    
    [Column("status_approval")]
    public StatusApprovalEnum StatusApproval { get; set; }

    // Cardinality
    public Employee? Employee { get; set; }
    public Interview? Interview { get; set; }
}
