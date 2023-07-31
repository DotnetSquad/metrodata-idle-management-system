using API.Contracts;
using API.DataTransferObjects.Employees;
using API.Models;
using API.Utilities.Enums;
using API.Utilities.Handlers;

namespace API.Services;

public class EmployeeService
{
    private IEmployeeRepository _employeeRepository;
    private IGradeRepository _gradeRepository;
    private IProfileRepository _profileRepository;

    public EmployeeService(IEmployeeRepository employeeRepository, IGradeRepository gradeRepository,
        IProfileRepository profileRepository)
    {
        _employeeRepository = employeeRepository;
        _gradeRepository = gradeRepository;
        _profileRepository = profileRepository;
    }

    public IEnumerable<EmployeeDtoGet> Get()
    {
        var employees = _employeeRepository.GetAll().ToList();
        if (!employees.Any()) return Enumerable.Empty<EmployeeDtoGet>();
        List<EmployeeDtoGet> employeeDtoGets = new();

        foreach (var employee in employees)
        {
            employeeDtoGets.Add((EmployeeDtoGet)employee);
        }

        return employeeDtoGets;
    }

    public EmployeeDtoGet? Get(Guid guid)
    {
        var employee = _employeeRepository.GetByGuid(guid);
        if (employee is null) return null!;

        return (EmployeeDtoGet)employee;
    }

    public EmployeeDtoCreate? Create(EmployeeDtoCreate employeeDtoCreate)
    {
        Employee employee = employeeDtoCreate;
        employee.Nik = GenerateHandler.GenerateNik(_employeeRepository.GetLastEmployeeNik());

        var grade = new Grade
        {
            Guid = Guid.NewGuid(),
            GradeLevel = GradeEnum.B,
            ScoreSegment1 = 0,
            ScoreSegment2 = 0,
            ScoreSegment3 = 0,
            ScoreSegment4 = 0,
            TotalScore = 0,
            CreatedDate = DateTime.Now
        };

        var gradeCreated = _gradeRepository.Create(grade);
        employee.GradeGuid = gradeCreated.Guid;

        var profile = new Profile
        {
            Guid = Guid.NewGuid(),
            Skills = "",
            Linkedin = "",
            Resume = "",
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now
        };

        var profileCreated = _profileRepository.Create(profile);
        employee.ProfileGuid = profileCreated.Guid;

        var employeeCreated = _employeeRepository.Create(employee);
        if (employeeCreated is null)
        {
            // delete when employee is null
            _gradeRepository.Delete(gradeCreated);
            _profileRepository.Delete(profileCreated);
            return null!;
        }

        return (EmployeeDtoCreate)employeeCreated;
    }

    public int Update(EmployeeDtoUpdate employeeDtoUpdate)
    {
        var employee = _employeeRepository.GetByGuid(employeeDtoUpdate.Guid);
        if (employee is null) return -1;

        var employeeUpdated = _employeeRepository.Update(employeeDtoUpdate);
        return !employeeUpdated ? 0 : 1;
    }

    public int Delete(Guid guid)
    {
        var employee = _employeeRepository.GetByGuid(guid);
        if (employee is null) return -1;

        var employeeDeleted = _employeeRepository.Delete(employee);
        return !employeeDeleted ? 0 : 1;
    }
}
