using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

[Table("tb_m_companies")]
public class Company : BaseEntity
{
    [Column("company_name", TypeName = "nvarchar(255)")]
    public string CompanyName { get; set; }

    [Column("description", TypeName = "nvarchar(255)")]
    public string Description { get; set; }

    [Column("address", TypeName = "nvarchar(255)")]
    public string Address { get; set; }

    // Cardinality
    public ICollection<Job>? Jobs { get; set; }
    public ICollection<Placement>? Placements { get; set; }

}
