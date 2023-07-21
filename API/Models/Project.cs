using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

[Table("tb_m_projects")]
public class Project : BaseEntity
{
    [Column("name_project", TypeName = "nvarchar(255)")]
    public string NameProject { get; set; }

    [Column("project_lead", TypeName = "nvarchar(100)")]
    public string ProjectLead { get; set; }

    [Column("description", TypeName = "nvarchar(255)")]
    public string Description { get; set; }

    // Cardinality
    public ICollection<EmployeeProject>? EmployeeProjects { get; set; }
}
