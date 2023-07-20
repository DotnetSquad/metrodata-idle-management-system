using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

[Table("tb_tr_employee_interview")]
public class EmployeeInterview
{
    [Column("employee_guid")]
    public Guid EmployeeGuid { get; set; }

    [Column("interview_guid")]
    public Guid InterviewGuid { get; set; }

    // Cardinality
    /*public Employee? Employees { get; set; }
    public Interview? Interviews { get; set; }*/
}
