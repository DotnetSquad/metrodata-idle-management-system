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

    public DashboardService(IEmployeeRepository employeeRepository, IInterviewRepository interviewRepository, IEmployeeJobRepository employeeJobRepository, IProjectRepository projectRepository, IEmployeeProjectRepository employeeProjectRepository, ICompanyRepository companyRepository, IPlacementRepository placementRepository)
    {
        _employeeRepository = employeeRepository;
        _interviewRepository = interviewRepository;
        _employeeJobRepository = employeeJobRepository;
        _projectRepository = projectRepository;
        _employeeProjectRepository = employeeProjectRepository;
        _companyRepository = companyRepository;
        _placementRepository = placementRepository;
    }

    public DashboardsDtoGetStatus GetEmployeeStatus()
    {
        var employees = _employeeRepository.GetAll();
        if (!employees.Any())
        {
            return null;
        }

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

        var interviewStatus = (from i in _interviewRepository.GetAll()
                               join ej in _employeeJobRepository.GetAll() on i.Guid equals ej.InterviewGuid
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

    public DashboardDtoGetInterviewStatus GetProjectStatus()
    {

        var interviewStatus = (from p in _projectRepository.GetAll()
                               join ep in _employeeProjectRepository.GetAll() on p.Guid equals ep.ProjectGuid
                               join e in _employeeRepository.GetAll() on ep.EmployeeGuid equals e.Guid
                               select new DashboardDtoGetInterviewStatus
                               {
                                   Accepted = ep.StatusApproval == StatusApprovalEnum.Accepted ? 1 : 0,
                                   Pending = ep.StatusApproval == StatusApprovalEnum.Pending ? 1 : 0,
                                   Rejected = ep.StatusApproval == StatusApprovalEnum.Rejected ? 1 : 0
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
