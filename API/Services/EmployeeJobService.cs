using API.Contracts;
using API.DataTransferObjects.EmployeeJobs;
using API.DataTransferObjects.EmployeeProjects;
using API.DataTransferObjects.Placements;
using API.Utilities.Enums;

namespace API.Services;

public class EmployeeJobService
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IEmployeeJobRepository _employeeJobRepository;
    private readonly IPlacementRepository _placementRepository;
    private readonly IJobRepository _jobRepository;

    public EmployeeJobService(ICompanyRepository companyRepository, IEmployeeJobRepository employeeJobRepository, IPlacementRepository placementRepository, IJobRepository jobRepository)
    {
        _companyRepository = companyRepository;
        _employeeJobRepository = employeeJobRepository;
        _placementRepository = placementRepository;
        _jobRepository = jobRepository;
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
        var job = _jobRepository.GetByGuid(employeeJobDtoUpdate.JobGuid);
        var company = _companyRepository.GetByGuid(job.CompanyGuid);
        if (employeeJobUpdated)
        {
            if (employeeJobDtoUpdate.StatusApproval == StatusApprovalEnum.Accepted)
            {
                var placementDtoCreate = new PlacementDtoCreate
                {
                    Title = $"{job.JobName} at {company.CompanyName}",
                    Description = job.Description,
                    EmployeeGuid = employeeJobDtoUpdate.EmployeeGuid,
                    CompanyGuid = company.Guid
                };
                var placementCreated = _placementRepository.Create(placementDtoCreate);
            }
            
            if (employeeJobDtoUpdate.StatusApproval == StatusApprovalEnum.Rejected)
            {
                var placement = _placementRepository.GetByEmployeeGuid(employeeJobDtoUpdate.EmployeeGuid);
                var placemrntDtoGets = (List<PlacementDtoGet>)placement;
                foreach (var placementDtoGet in placemrntDtoGets)
                {
                    if (placementDtoGet.EmployeeGuid == employeeJobDtoUpdate.EmployeeGuid && placementDtoGet.CompanyGuid == company.Guid) _placementRepository.Delete(placementDtoGet);
                    
                }
            }
        }
        return employeeJobUpdated ? 1 : 0;
    }

    public int Delete(Guid guid)
    {
        var employeeJob = _employeeJobRepository.GetByGuid(guid);
        if (employeeJob is null) return -1;

        var employeeJobDeleted = _employeeJobRepository.Delete(employeeJob);
        
        var placement = _placementRepository.GetByEmployeeGuid(employeeJob.EmployeeGuid);
        var placementDtoGets = (List<PlacementDtoGet>)placement;
        
        var job = _jobRepository.GetByGuid(employeeJob.JobGuid);
        var company = _companyRepository.GetByGuid(job.CompanyGuid);

        if (employeeJobDeleted)
        {
            foreach (var placementDtoGet in placementDtoGets)
            {
                if (placementDtoGet.EmployeeGuid == employeeJob.EmployeeGuid && placementDtoGet.CompanyGuid == company.Guid) _placementRepository.Delete(placementDtoGet);
                    
            }
        }
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
