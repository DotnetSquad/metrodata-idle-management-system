using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace metrodata_idle_management_system_API.Models;

public abstract class BaseEntity
{
    [Key] [Column("guid")] public Guid Guid { get; set; }

    [Column("created_date")] public DateTime CreatedDate { get; set; }

    [Column("modified_date")] public DateTime ModifiedDate { get; set; }
}
