using Client.Utilities.Enums;

namespace Client.DataTransferObjects.Profiles;

public class EmployeeProfileDtoGet
{
    public Guid EmployeeGuid { get; set; }
    public string Nik { get; set; }
    public string FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public GenderEnum Gender { get; set; }
    public DateTime HiringDate { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public StatusEnum Status { get; set; }
    public Guid? GradeGuid { get; set; }
    public Guid? ProfileGuidInEmployee { get; set; }
    
    public Guid ProfileGuid { get; set; }
    public string Skills { get; set; }
    public string Linkedin { get; set; }
    public string Resume { get; set; }
}
