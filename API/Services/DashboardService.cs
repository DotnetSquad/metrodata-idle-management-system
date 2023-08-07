using API.Contracts;
using API.DataTransferObjects.Dashboards;
using API.Utilities.Enums;

namespace API.Services;

public class DashboardService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IEmployeeJobRepository _employeeJobRepository;
    private readonly IInterviewRepository _interviewRepository;
    private readonly IEmployeeProjectRepository _employeeProjectRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly IPlacementRepository _placementRepository;
    private readonly IJobRepository _jobRepository;

    public DashboardService(IEmployeeRepository employeeRepository, IInterviewRepository interviewRepository, IEmployeeJobRepository employeeJobRepository, IProjectRepository projectRepository, IEmployeeProjectRepository employeeProjectRepository, ICompanyRepository companyRepository, IPlacementRepository placementRepository, IJobRepository jobRepository)
    {
        _employeeRepository = employeeRepository;
        _interviewRepository = interviewRepository;
        _employeeJobRepository = employeeJobRepository;
        _projectRepository = projectRepository;
        _employeeProjectRepository = employeeProjectRepository;
        _companyRepository = companyRepository;
        _placementRepository = placementRepository;
        _jobRepository = jobRepository;
    }

    public DashboardsDtoGetStatus GetEmployeeStatus()
    {
        int idleEmployeeCount = _employeeRepository.GetIdleEmployeeStatus();
        int workingEmployeeCount = _employeeRepository.GetWorkingEmployeeStatus();

        var status = new DashboardsDtoGetStatus
        {
            Idle = idleEmployeeCount,
            Working = workingEmployeeCount
        };

        return status;
    }

    public DashboardDtoGetInterviewStatus GetInterviewStatus()
    {

        var interviewStatus = (from j in _jobRepository.GetAll()
                               join ej in _employeeJobRepository.GetAll() on j.Guid equals ej.JobGuid
                               join e in _employeeRepository.GetAll() on ej.EmployeeGuid equals e.Guid
                               select new DashboardDtoGetInterviewStatus
                               {
                                   Accepted = ej.StatusApproval == StatusApprovalEnum.Accepted ? 1 : 0,
                                   Pending = ej.StatusApproval == StatusApprovalEnum.Pending ? 1 : 0,
                                   Rejected = ej.StatusApproval == StatusApprovalEnum.Rejected ? 1 : 0
                               });

        if (!interviewStatus.Any())
        {
            return null;
        }

        var status = new DashboardDtoGetInterviewStatus
        {
            Accepted = interviewStatus.Sum(i => i.Accepted),
            Pending = interviewStatus.Sum(i => i.Pending),
            Rejected = interviewStatus.Sum(i => i.Rejected)
        };

        return status;
    }

    public DashboardDtoGetInterviewStatus GetStatus()
    {
        var jobStatus = (from j in _jobRepository.GetAll()
                         join ej in _employeeJobRepository.GetAll() on j.Guid equals ej.JobGuid
                         join e in _employeeRepository.GetAll() on ej.EmployeeGuid equals e.Guid
                         select new DashboardDtoGetInterviewStatus
                         {
                             Accepted = ej.StatusApproval == StatusApprovalEnum.Accepted ? 1 : 0,
                             Pending = ej.StatusApproval == StatusApprovalEnum.Pending ? 1 : 0,
                             Rejected = ej.StatusApproval == StatusApprovalEnum.Rejected ? 1 : 0
                         });

        var projectStatus = (from p in _projectRepository.GetAll()
                             join ep in _employeeProjectRepository.GetAll() on p.Guid equals ep.ProjectGuid
                             join e in _employeeRepository.GetAll() on ep.EmployeeGuid equals e.Guid
                             select new DashboardDtoGetInterviewStatus
                             {
                                 Accepted = ep.StatusApproval == StatusApprovalEnum.Accepted ? 1 : 0,
                                 Pending = ep.StatusApproval == StatusApprovalEnum.Pending ? 1 : 0,
                                 Rejected = ep.StatusApproval == StatusApprovalEnum.Rejected ? 1 : 0
                             });

        if (!jobStatus.Any() && !projectStatus.Any())
        {
            return null;
        }

        var totalAccepted = (jobStatus?.Sum(i => i.Accepted) ?? 0) + (projectStatus?.Sum(p => p.Accepted) ?? 0);
        var totalPending = (jobStatus?.Sum(i => i.Pending) ?? 0) + (projectStatus?.Sum(p => p.Pending) ?? 0);
        var totalRejected = (jobStatus?.Sum(i => i.Rejected) ?? 0) + (projectStatus?.Sum(p => p.Rejected) ?? 0);

        var status = new DashboardDtoGetInterviewStatus
        {
            Accepted = totalAccepted,
            Pending = totalPending,
            Rejected = totalRejected
        };

        return status;
    }

    public IEnumerable<DashboardDtoGetClient> GetTop5Client()
    {
        var getClient = _placementRepository.GetAll()
            .GroupBy(pl => pl.CompanyGuid)
            .Select(group => new DashboardDtoGetClient
            {
                CompanyGuid = group.Key,
                TotalEmployees = group.Count()
            })
            .OrderByDescending(x => x.TotalEmployees)
            .Take(5);

        return getClient;
    }
}
