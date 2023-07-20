using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

[Table("tb_m_projects")]
public class Project : BaseEntity
{
    [Column("name_project")]
    public string NameProject { get; set; }

    [Column("project_lead")]
    public string ProjectLead { get; set; }

    [Column("description")]
    public string Description { get; set; }
}
