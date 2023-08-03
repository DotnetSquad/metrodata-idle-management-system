using API.Contracts;
using API.DataTransferObjects.Dashboards;
using API.Utilities.Enums;

namespace API.Services;

public class DashboardService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IEmployeeJobRepository _employeeInterviewRepository;
    private readonly IInterviewRepository _interviewRepository;

    public DashboardService(IEmployeeRepository employeeRepository, IInterviewRepository interviewRepository, IEmployeeJobRepository employeeInterviewRepository)
    {
        _employeeRepository = employeeRepository;
        _interviewRepository = interviewRepository;
        _employeeInterviewRepository = employeeInterviewRepository;
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
                               join er in _employeeInterviewRepository.GetAll() on i.Guid equals er.InterviewGuid
                               join e in _employeeRepository.GetAll() on er.EmployeeGuid equals e.Guid
                               select new DashboardDtoGetInterviewStatus
                               {
                                   Accepted = er.StatusApproval == StatusApprovalEnum.Accepted ? 1 : 0,
                                   Pending = er.StatusApproval == StatusApprovalEnum.Pending ? 1 : 0,
                                   Rejected = er.StatusApproval == StatusApprovalEnum.Rejected ? 1 : 0
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
}
