using API.Contracts;
using API.DataTransferObjects.Dashboards;

namespace API.Services;

public class DashboardService
{
    private IEmployeeRepository _employeeRepository;

    public DashboardService(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
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
}
