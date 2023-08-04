using API.Contracts;
using API.DataTransferObjects.EmployeeProjects;
using API.Utilities.Enums;

namespace API.Services;

public class EmployeeProjectService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IEmployeeProjectRepository _employeeProjectRepository;

    public EmployeeProjectService(IEmployeeRepository employeeRepository, IEmployeeProjectRepository employeeProjectRepository)
    {
        _employeeRepository = employeeRepository;
        _employeeProjectRepository = employeeProjectRepository;
    }

    public IEnumerable<EmployeeProjectDtoGet> Get()
    {
        var employeeProjects = _employeeProjectRepository.GetAll().ToList();
        if (!employeeProjects.Any()) return Enumerable.Empty<EmployeeProjectDtoGet>();
        List<EmployeeProjectDtoGet> employeeProjectDtoGets = new();

        foreach (var employeeProject in employeeProjects)
        {
            employeeProjectDtoGets.Add((EmployeeProjectDtoGet)employeeProject);
        }

        return employeeProjectDtoGets;
    }

    public EmployeeProjectDtoGet? Get(Guid guid)
    {
        var employeeProject = _employeeProjectRepository.GetByGuid(guid);
        if (employeeProject is null) return null!;

        return (EmployeeProjectDtoGet)employeeProject;
    }

    public EmployeeProjectDtoCreate? Create(EmployeeProjectDtoCreate employeeProjectDtoCreate)
    {
        var employeeProjectCreated = _employeeProjectRepository.Create(employeeProjectDtoCreate);
        if (employeeProjectCreated is null) return null!;

        return (EmployeeProjectDtoCreate)employeeProjectCreated;
    }

    public int Update(EmployeeProjectDtoUpdate employeeProjectDtoUpdate)
    {
        var employeeProject = _employeeProjectRepository.GetByGuid(employeeProjectDtoUpdate.Guid);
        if (employeeProject is null) return -1;

        var employeeProjectUpdated = _employeeProjectRepository.Update(employeeProjectDtoUpdate);
        var employee = _employeeRepository.GetByGuid(employeeProjectDtoUpdate.EmployeeGuid);

        if (employeeProjectUpdated && employeeProjectDtoUpdate.StatusApproval == StatusApprovalEnum.Accepted)
        {
            employee.Status = StatusEnum.Working;
            _employeeRepository.Update(employee);
        }
        else if (employeeProjectUpdated && employeeProjectDtoUpdate.StatusApproval == StatusApprovalEnum.Rejected)
        {
            employee.Status = StatusEnum.Idle;
            _employeeRepository.Update(employee);
        }
        
        return !employeeProjectUpdated ? 0 : 1;
    }

    public int Delete(Guid guid)
    {
        var employeeProject = _employeeProjectRepository.GetByGuid(guid);
        if (employeeProject is null) return -1;

        var employeeProjectDeleted = _employeeProjectRepository.Delete(employeeProject);
        var employee = _employeeRepository.GetByGuid(employeeProject.EmployeeGuid);
        
        if (employeeProjectDeleted)
        {
            employee.Status = StatusEnum.Idle;
            _employeeRepository.Update(employee);
        }
        
        return employeeProjectDeleted ? 1 : 0;
    }
    
    public IEnumerable<EmployeeProjectDtoGet> GetByProject(Guid projectGuid)
    {
        var allProjects = _employeeProjectRepository.GetAll();
        var projectsByGuid = allProjects.Where(employeeProject => employeeProject.ProjectGuid == projectGuid);
        
        List<EmployeeProjectDtoGet> projectDtoGets = new();

        foreach (var project in projectsByGuid)
        {
            projectDtoGets.Add((EmployeeProjectDtoGet)project);
        }

        return projectDtoGets;
    }
}
