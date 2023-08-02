using API.Contracts;
using API.DataTransferObjects.EmployeeProjects;

namespace API.Services;

public class EmployeeProjectService
{
    private IEmployeeProjectRepository _employeeProjectRepository;

    public EmployeeProjectService(IEmployeeProjectRepository employeeProjectRepository)
    {
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
        return !employeeProjectUpdated ? 0 : 1;
    }

    public int Delete(Guid guid)
    {
        var employeeProject = _employeeProjectRepository.GetByGuid(guid);
        if (employeeProject is null) return -1;

        var employeeProjectDeleted = _employeeProjectRepository.Delete(employeeProject);
        return !employeeProjectDeleted ? 0 : 1;
    }
}
