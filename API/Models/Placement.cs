using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

[Table("tb_m_placements")]
public class Placement : BaseEntity
{
    [Column("title", TypeName = "nvarchar(255)")]
    public string Title { get; set; }

    [Column("description", TypeName = "nvarchar(255)")]
    public string Description { get; set; }

    [Column("employee_guid")]
    public Guid EmployeeGuid { get; set; }

    [Column("profile_guid")]
    public Guid ProfileGuid { get; set; }

    // Cardinality
    /*public Employee? Employee { get; set; }
    public Company? Company { get; set; }*/
}
