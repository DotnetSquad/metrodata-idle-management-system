using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

[Table("tb_m_jobs")]
public class Job : BaseEntity
{
    [Column("job_name", TypeName = "nvarchar(255)")]
    public string JobName { get; set; }

    [Column("description", TypeName = "nvarchar(255)")]
    public string Description { get; set; }

    [Column("company_guid")]
    public Guid CompanyGuid { get; set; }

    // Cardinality
    /*public Company? Company { get; set; }
    public ICollection<Interview>? Interviews { get; set; }*/
}
