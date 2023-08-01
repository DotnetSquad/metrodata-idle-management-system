using API.Contracts;
using API.DataTransferObjects.Employees;
using API.Models;
using API.Utilities.Enums;
using API.Utilities.Handlers;

namespace API.Services;

public class EmployeeService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IAccountRoleRepository _accountRoleRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IGradeRepository _gradeRepository;
    private readonly IProfileRepository _profileRepository;
    private readonly IRoleRepository _roleRepository;

    public EmployeeService(IAccountRepository accountRepository, IAccountRoleRepository accountRoleRepository,
        IEmployeeRepository employeeRepository, IGradeRepository gradeRepository,
        IProfileRepository profileRepository, IRoleRepository roleRepository)
    {
        _accountRepository = accountRepository;
        _accountRoleRepository = accountRoleRepository;
        _employeeRepository = employeeRepository;
        _gradeRepository = gradeRepository;
        _profileRepository = profileRepository;
        _roleRepository = roleRepository;
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

    public IEnumerable<EmployeeDtoGet> GetByRole(Guid roleGuid)
    {
        var employeesByRole = (from employee in _employeeRepository.GetAll()
            join account in _accountRepository.GetAll() on employee.Guid equals account.Guid
            join accountRole in _accountRoleRepository.GetAll() on account.Guid equals accountRole.AccountGuid
            join roleRepository in _roleRepository.GetAll() on accountRole.RoleGuid equals roleRepository.Guid
            where accountRole.RoleGuid == roleGuid
            select new EmployeeDtoGet()
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
                ProfileGuid = employee.ProfileGuid,
            }).ToList();

        return employeesByRole;
    }

    public IEnumerable<EmployeeDtoGet> GetExcludeRole(Guid roleGuid)
    {
        var employeesByRole = GetByRole(roleGuid);
        var employees = _employeeRepository.GetAll();

        var employeesByRoleGuid = employeesByRole.Select(employee => employee.Guid).ToList();
        var employeesExcludeRole = employees.Where(employee => !employeesByRoleGuid.Contains(employee.Guid)).ToList();

        if (!employeesExcludeRole.Any()) return Enumerable.Empty<EmployeeDtoGet>();
        List<EmployeeDtoGet> employeeDtoGets = new();

        foreach (var employee in employeesExcludeRole)
        {
            employeeDtoGets.Add((EmployeeDtoGet)employee);
        }

        return employeeDtoGets;
    }
}
