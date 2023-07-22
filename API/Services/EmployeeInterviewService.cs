using API.Contracts;
using API.DataTransferObjects.EmployeeInterviews;

namespace API.Services;

public class EmployeeInterviewService
{
    private IEmployeeInterviewRepository _employeeInterviewRepository;
    
    public EmployeeInterviewService(IEmployeeInterviewRepository employeeInterviewRepository)
    {
        _employeeInterviewRepository = employeeInterviewRepository;
    }
    
    public IEnumerable<EmployeeInterviewDtoGet> Get()
    {
        var employeeInterviews = _employeeInterviewRepository.GetAll().ToList();
        if (!employeeInterviews.Any()) return Enumerable.Empty<EmployeeInterviewDtoGet>();
        List<EmployeeInterviewDtoGet> employeeInterviewDtoGets = new();
        
        foreach (var employeeInterview in employeeInterviews)
        {
            employeeInterviewDtoGets.Add((EmployeeInterviewDtoGet)employeeInterview);
        }
        
        return employeeInterviewDtoGets;
    }
    
    public EmployeeInterviewDtoGet? Get(Guid guid)
    {
        var employeeInterview = _employeeInterviewRepository.GetByGuid(guid);
        if (employeeInterview is null) return null!;
        
        return (EmployeeInterviewDtoGet)employeeInterview;
    }
    
    public EmployeeInterviewDtoCreate? Create(EmployeeInterviewDtoCreate employeeInterviewDtoCreate)
    {
        var employeeInterviewCreated = _employeeInterviewRepository.Create(employeeInterviewDtoCreate);
        if (employeeInterviewCreated is null) return null!;
        
        return (EmployeeInterviewDtoCreate)employeeInterviewCreated;
    }
    
    public int Update(EmployeeInterviewDtoUpdate employeeInterviewDtoUpdate)
    {
        var employeeInterview = _employeeInterviewRepository.GetByGuid(employeeInterviewDtoUpdate.Guid);
        if (employeeInterview is null) return -1;
        
        var employeeInterviewUpdated = _employeeInterviewRepository.Update(employeeInterviewDtoUpdate);
        return !employeeInterviewUpdated ? 0 : 1;
    }
    
    public int Delete(Guid guid)
    {
        var employeeInterview = _employeeInterviewRepository.GetByGuid(guid);
        if (employeeInterview is null) return -1;
        
        var employeeInterviewDeleted = _employeeInterviewRepository.Delete(employeeInterview);
        return !employeeInterviewDeleted ? 0 : 1;
    }
}
