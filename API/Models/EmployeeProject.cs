using System.ComponentModel.DataAnnotations.Schema;
using API.Utilities.Enums;

namespace API.Models;

[Table("tb_tr_employee_project")]
public class EmployeeProject : BaseEntity
{
    [Column("employee_guid")]
    public Guid EmployeeGuid { get; set; }

    [Column("project_guid")]
    public Guid ProjectGuid { get; set; }
    
    [Column("status_approval")]
    public StatusApprovalEnum StatusApproval { get; set; }

    // Cardinality
    public Employee? Employee { get; set; }

    public Project? Project { get; set; }
}
