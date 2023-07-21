using API.Models;
using API.Utilities.Enums;

namespace API.DataTransferObjects.Employees;

public class EmployeeDtoGet
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
    public static implicit operator Employee(EmployeeDtoGet employeeDtoGet)
    {
        return new Employee
        {
            Guid = employeeDtoGet.Guid,
            Nik = employeeDtoGet.Nik,
            FirstName = employeeDtoGet.FirstName,
            LastName = employeeDtoGet.LastName,
            BirthDate = employeeDtoGet.BirthDate,
            Gender = employeeDtoGet.Gender,
            HiringDate = employeeDtoGet.HiringDate,
            Email = employeeDtoGet.Email,
            PhoneNumber = employeeDtoGet.PhoneNumber,
            Status = employeeDtoGet.Status,
            GradeGuid = employeeDtoGet.GradeGuid,
            ProfileGuid = employeeDtoGet.ProfileGuid
        };
    }

    // explicit operator
    public static explicit operator EmployeeDtoGet(Employee employee)
    {
        return new EmployeeDtoGet
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
