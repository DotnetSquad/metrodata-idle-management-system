using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

[Table("tb_m_employee_project")]
public class EmployeeProject : BaseEntity
{
    [Column("employee_guid")]
    public Guid EmployeeGuid { get; set; }

    [Column("project_guid")]
    public Guid ProjectGuid { get; set; }
}
