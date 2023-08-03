using API.Contracts;
using API.DataTransferObjects.EmployeeJobs;
using API.DataTransferObjects.EmployeeProjects;

namespace API.Services;

public class EmployeeJobService
{
    private readonly IEmployeeJobRepository _employeeJobRepository;

    public EmployeeJobService(IEmployeeJobRepository employeeJobRepository)
    {
        _employeeJobRepository = employeeJobRepository;
    }

    public IEnumerable<EmployeeJobDtoGet> Get()
    {
        var employeeJobs = _employeeJobRepository.GetAll().ToList();
        if (!employeeJobs.Any()) return Enumerable.Empty<EmployeeJobDtoGet>();
        List<EmployeeJobDtoGet> employeeJobDtoGets = new();

        foreach (var employeeJob in employeeJobs)
        {
            employeeJobDtoGets.Add((EmployeeJobDtoGet)employeeJob);
        }

        return employeeJobDtoGets;
    }

    public EmployeeJobDtoGet? Get(Guid guid)
    {
        var employeeJob = _employeeJobRepository.GetByGuid(guid);
        if (employeeJob is null) return null!;

        return (EmployeeJobDtoGet)employeeJob;
    }

    public EmployeeJobDtoCreate? Create(EmployeeJobDtoCreate employeeJobDtoCreate)
    {
        var employeeJobCreated = _employeeJobRepository.Create(employeeJobDtoCreate);
        if (employeeJobCreated is null) return null!;

        return (EmployeeJobDtoCreate)employeeJobCreated;
    }

    public int Update(EmployeeJobDtoUpdate employeeJobDtoUpdate)
    {
        var employeeJob = _employeeJobRepository.GetByGuid(employeeJobDtoUpdate.Guid);
        if (employeeJob is null) return -1;

        var employeeJobUpdated = _employeeJobRepository.Update(employeeJobDtoUpdate);
        return !employeeJobUpdated ? 0 : 1;
    }

    public int Delete(Guid guid)
    {
        var employeeJob = _employeeJobRepository.GetByGuid(guid);
        if (employeeJob is null) return -1;

        var employeeJobDeleted = _employeeJobRepository.Delete(employeeJob);
        return !employeeJobDeleted ? 0 : 1;
    }
    
    public IEnumerable<EmployeeJobDtoGet> GetByJob(Guid jobGuid)
    {
        var allJobs = _employeeJobRepository.GetAll();
        var employeeJobs = allJobs.Where(employeeJob => employeeJob.JobGuid == jobGuid);
        
        List<EmployeeJobDtoGet> employeeJobDtoGets = new();

        foreach (var employeeJob in employeeJobs)
        {
            employeeJobDtoGets.Add((EmployeeJobDtoGet)employeeJob);
        }

        return employeeJobDtoGets;
    }
}
