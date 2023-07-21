using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

[Table("tb_m_profiles")]
public class Profile : BaseEntity
{
    [Column("skills", TypeName = "nvarchar(255)")]
    public string Skills { get; set; }

    [Column("linkedin", TypeName = "nvarchar(255)")]
    public string Linkedin { get; set; }

    [Column("resume", TypeName = "nvarchar(255)")]
    public string Resume { get; set; }

    // Cardinality
    public Employee? Employee { get; set; }
}
