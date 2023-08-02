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
    private readonly IEmployeeProjectRepository _employeeProjectRepository;
    private readonly IGradeRepository _gradeRepository;
    private readonly IProfileRepository _profileRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly IRoleRepository _roleRepository;

    public EmployeeService(IAccountRepository accountRepository, IAccountRoleRepository accountRoleRepository,
        IEmployeeRepository employeeRepository, IGradeRepository gradeRepository,
        IProfileRepository profileRepository, IRoleRepository roleRepository, IProjectRepository projectRepository, IEmployeeProjectRepository employeeProjectRepository)
    {
        _accountRepository = accountRepository;
        _accountRoleRepository = accountRoleRepository;
        _employeeRepository = employeeRepository;
        _gradeRepository = gradeRepository;
        _profileRepository = profileRepository;
        _roleRepository = roleRepository;
        _projectRepository = projectRepository;
        _employeeProjectRepository = employeeProjectRepository;
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
    
    public EmployeeDtoGet? GetByEmail(string email)
    {
        var employee = _employeeRepository.GetByEmail(email);
        if (employee is null) return null!;

        return (EmployeeDtoGet)employee;
    }
  
  public IEnumerable<EmployeeDtoGet> GetEmployeeByProject(Guid projectGuid)
    {
        var employeesByProject = (from employee in _employeeRepository.GetAll()
                                  join employeeProject in _employeeProjectRepository.GetAll()
                                  on employee.Guid equals employeeProject.EmployeeGuid
                                  join project in _projectRepository.GetAll()
                                  on employeeProject.ProjectGuid equals project.Guid
                                  where employeeProject.ProjectGuid == projectGuid
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
        return employeesByProject;
    }

    public IEnumerable<EmployeeDtoGet> GetExcludeProject(Guid projectGuid)
    {
        var employeesByProject = GetEmployeeByProject(projectGuid).ToList();

        var employeesExcludeProject = (from employee in _employeeRepository.GetAll()
                                       join employeeProject in _employeeProjectRepository.GetAll()
                                       on employee.Guid equals employeeProject.EmployeeGuid into employeeProjectsGroup
                                       from employeeProject in employeeProjectsGroup.DefaultIfEmpty()
                                       where employeeProject == null || employeeProject.ProjectGuid != projectGuid
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

        var employeesExcludeProjectFiltered = employeesExcludeProject.Where(e => !employeesByProject.Any(r => r.Guid == e.Guid)).Distinct(new EmployeeDtoGetComparer());

        return employeesExcludeProjectFiltered;
    }

    public class EmployeeDtoGetComparer : IEqualityComparer<EmployeeDtoGet>
    {
        public bool Equals(EmployeeDtoGet x, EmployeeDtoGet y)
        {
            // Assuming that the Guid property uniquely identifies an employee
            return x.Guid == y.Guid;
        }

        public int GetHashCode(EmployeeDtoGet obj)
        {
            return obj.Guid.GetHashCode();
        }
    }
}
