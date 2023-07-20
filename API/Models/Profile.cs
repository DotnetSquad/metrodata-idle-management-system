using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

[Table("tb_m_profiles")]
public class Profile : BaseEntity
{
    [Column("skills")]
    public string skills { get; set; }

    [Column("linkedin")]
    public string linkedin { get; set; }

    [Column("resume")]
    public string resume { get; set; }
}
