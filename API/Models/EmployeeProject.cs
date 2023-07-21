using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

[Table("tb_tr_employee_project")]
public class EmployeeProject : BaseEntity
{
    [Column("employee_guid")]
    public Guid EmployeeGuid { get; set; }

    [Column("project_guid")]
    public Guid ProjectGuid { get; set; }

    // Cardinality
    public Employee? Employee { get; set; }

    public Project? Project { get; set; }
}
