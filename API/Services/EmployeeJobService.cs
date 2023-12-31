using API.Contracts;
using API.DataTransferObjects.EmployeeJobs;
using API.DataTransferObjects.Interviews;
using API.DataTransferObjects.Placements;
using API.Utilities.Enums;

namespace API.Services;

public class EmployeeJobService
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IEmployeeJobRepository _employeeJobRepository;
    private readonly IPlacementRepository _placementRepository;
    private readonly IInterviewRepository _interviewRepository;
    private readonly IJobRepository _jobRepository;
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeJobService(ICompanyRepository companyRepository, IEmployeeJobRepository employeeJobRepository, IPlacementRepository placementRepository, IInterviewRepository interviewRepository, IJobRepository jobRepository, IEmployeeRepository employeeRepository)
    {
        _companyRepository = companyRepository;
        _employeeJobRepository = employeeJobRepository;
        _placementRepository = placementRepository;
        _interviewRepository = interviewRepository;
        _jobRepository = jobRepository;
        _employeeRepository = employeeRepository;
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
        var job = _jobRepository.GetByGuid(employeeJobDtoCreate.JobGuid);
        var company = _companyRepository.GetByGuid(job.CompanyGuid);
        var employee = _employeeRepository.GetByGuid(employeeJobDtoCreate.EmployeeGuid);


        var interviewDtoCreate = new InterviewDtoCreate
        {
            Title = $"{job.JobName} at {company.CompanyName} - {employee.Nik} {employee.FirstName} {employee.LastName}",
            Link = "",
            InterviewDate = DateTime.Now,
            Description = ""
        };

        var interviewCreated = _interviewRepository.Create(interviewDtoCreate);
        employeeJobDtoCreate.InterviewGuid = interviewCreated.Guid;
        var employeeJobCreated = _employeeJobRepository.Create(employeeJobDtoCreate);

        if (employeeJobCreated is null) return null!;

        return (EmployeeJobDtoCreate)employeeJobCreated;
    }

    public int Update(EmployeeJobDtoUpdate employeeJobDtoUpdate)
    {
        var employeeJob = _employeeJobRepository.GetByGuid(employeeJobDtoUpdate.Guid);
        if (employeeJob is null) return -1;

        var employeeJobUpdated = _employeeJobRepository.Update(employeeJobDtoUpdate);
        var employee = _employeeRepository.GetByGuid(employeeJobDtoUpdate.EmployeeGuid);
        var job = _jobRepository.GetByGuid(employeeJobDtoUpdate.JobGuid);
        var company = _companyRepository.GetByGuid(job.CompanyGuid);
        if (employeeJobUpdated)
        {
            if (employeeJobDtoUpdate.StatusApproval == StatusApprovalEnum.Accepted)
            {
                employee.Status = StatusEnum.Working;
                _employeeRepository.Update(employee);
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
                employee.Status = StatusEnum.Idle;
                _employeeRepository.Update(employee);
                var placement = _placementRepository.GetByEmployeeGuid(employeeJobDtoUpdate.EmployeeGuid);
                var placementDtoGets = placement;

                if (placementDtoGets != null)
                {
                    if (placementDtoGets.EmployeeGuid == employeeJobDtoUpdate.EmployeeGuid && placementDtoGets.CompanyGuid == company.Guid) _placementRepository.Delete(placementDtoGets);
                }
            }
        }
        return employeeJobUpdated ? 1 : 0;
    }

    public int Delete(Guid guid)
    {
        var employeeJob = _employeeJobRepository.GetByGuid(guid);
        if (employeeJob is null) return -1;
        var employee = _employeeRepository.GetByGuid(employeeJob.EmployeeGuid);

        var employeeJobDeleted = _employeeJobRepository.Delete(employeeJob);

        var placement = _placementRepository.GetByEmployeeGuid(employeeJob.EmployeeGuid);
        var placementDtoGets = placement;

        var job = _jobRepository.GetByGuid(employeeJob.JobGuid);
        var company = _companyRepository.GetByGuid(job.CompanyGuid);
        var interview = _interviewRepository.GetByGuid(employeeJob.InterviewGuid);

        if (employeeJobDeleted)
        {
            employee.Status = StatusEnum.Idle;
            var employeeUpdated = _employeeRepository.Update(employee);
            _interviewRepository.Delete(interview);
            if (placementDtoGets != null)
            {
                if (placementDtoGets.EmployeeGuid == employeeJob.EmployeeGuid && placementDtoGets.CompanyGuid == company.Guid)
                    _placementRepository.Delete(placementDtoGets);
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
