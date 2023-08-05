using API.Utilities.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

[Table("tb_m_employees")]
public class Employee : BaseEntity
{
    [Column("nik", TypeName = "nvarchar(6)")]
    public string Nik { get; set; }

    [Column("first_name", TypeName = "nvarchar(100)")]
    public string FirstName { get; set; }

    [Column("last_name", TypeName = "nvarchar(100)")]
    public string? LastName { get; set; }

    [Column("birth_date")]
    public DateTime BirthDate { get; set; }

    [Column("gender")]
    public GenderEnum Gender { get; set; }

    [Column("hiring_date")]
    public DateTime HiringDate { get; set; }

    [Column("status")]
    public StatusEnum Status { get; set; }

    [Column("email", TypeName = "nvarchar(50)")]
    public string Email { get; set; }

    [Column("phone_number", TypeName = "nvarchar(20)")]
    public string PhoneNumber { get; set; }

    [Column("grade_guid")]
    public Guid GradeGuid { get; set; }

    [Column("profile_guid")]
    public Guid ProfileGuid { get; set; }

    // Cardinality
    public Account? Account { get; set; }

    public ICollection<EmployeeJob>? EmployeeInterviews { get; set; }

    public ICollection<EmployeeProject>? EmployeeProjects { get; set; }

    public Grade? Grade { get; set; }

    public Placement? Placement { get; set; }

    public Profile? Profile { get; set; }
}
