using API.Models;
using API.Utilities.Enums;

namespace API.DataTransferObjects.Employees;

public class EmployeeDtoCreate
{
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
    public Guid? ProfileGuid { get; set; }
    
    // implicit operator
    public static implicit operator Employee(EmployeeDtoCreate employeeDtoCreate)
    {
        return new Employee
        {
            Nik = employeeDtoCreate.Nik,
            FirstName = employeeDtoCreate.FirstName,
            LastName = employeeDtoCreate.LastName,
            BirthDate = employeeDtoCreate.BirthDate,
            Gender = employeeDtoCreate.Gender,
            HiringDate = employeeDtoCreate.HiringDate,
            Email = employeeDtoCreate.Email,
            PhoneNumber = employeeDtoCreate.PhoneNumber,
            Status = employeeDtoCreate.Status,
            GradeGuid = employeeDtoCreate.GradeGuid,
            ProfileGuid = employeeDtoCreate.ProfileGuid
        };
    }
    
    // explicit operator
    public static explicit operator EmployeeDtoCreate(Employee employee)
    {
        return new EmployeeDtoCreate
        {
            Nik = employee.Nik,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            BirthDate = employee.BirthDate,
            Gender = employee.Gender,
            HiringDate = employee.HiringDate,
            Email = employee.Email,
            PhoneNumber = employee.PhoneNumber,
            Status = employee.Status,
            GradeGuid = employee.GradeGuid,
            ProfileGuid = employee.ProfileGuid
        };
    }
}
