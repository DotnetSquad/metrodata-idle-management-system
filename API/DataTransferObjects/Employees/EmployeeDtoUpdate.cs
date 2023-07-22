using API.Models;
using API.Utilities.Enums;

namespace API.DataTransferObjects.Employees;

public class EmployeeDtoUpdate
{
    public Guid Guid { get; set; }
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
    public static implicit operator Employee(EmployeeDtoUpdate employeeDtoUpdate)
    {
        return new Employee
        {
            Guid = employeeDtoUpdate.Guid,
            Nik = employeeDtoUpdate.Nik,
            FirstName = employeeDtoUpdate.FirstName,
            LastName = employeeDtoUpdate.LastName,
            BirthDate = employeeDtoUpdate.BirthDate,
            Gender = employeeDtoUpdate.Gender,
            HiringDate = employeeDtoUpdate.HiringDate,
            Email = employeeDtoUpdate.Email,
            PhoneNumber = employeeDtoUpdate.PhoneNumber,
            Status = employeeDtoUpdate.Status,
            GradeGuid = employeeDtoUpdate.GradeGuid,
            ProfileGuid = employeeDtoUpdate.ProfileGuid
        };
    }

    // explicit operator
    public static explicit operator EmployeeDtoUpdate(Employee employee)
    {
        return new EmployeeDtoUpdate
        {
            Guid = employee.Guid,
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
