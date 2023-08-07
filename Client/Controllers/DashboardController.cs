using Client.Contracts;
using Client.DataTransferObjects.Dashboards;
using Client.DataTransferObjects.Grades;
using Client.Utilities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers;

[Authorize]
public class DashboardController : Controller
{
    public string isNotCollapsed = "DashboardController";
    private readonly IDashboardRepository _dashboardRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IGradeRepository _gradeRepository;

    public DashboardController(IDashboardRepository dashboardRepository, IEmployeeRepository employeeRepository, IGradeRepository gradeRepository)
    {
        _dashboardRepository = dashboardRepository;
        _employeeRepository = employeeRepository;
        _gradeRepository = gradeRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var guidEmployee = User.Claims.FirstOrDefault(x => x.Type == "Guid")?.Value;
        var guid = Guid.Parse(guidEmployee ?? string.Empty);
        var employee = await _employeeRepository.Get(guid);

        ViewData["Employee"] = employee.Data;

        var grade = await _gradeRepository.Get(employee.Data.GradeGuid);
        var statisticEmployees = await _dashboardRepository.GetStatisticEmployee();
        var statisticInterviewStatus = await _dashboardRepository.GetStatisticInterviewStatus();

        ViewData["isNotCollapsed"] = isNotCollapsed;
        switch (statisticInterviewStatus.Code)
        {
            case 200:
                ViewData["statisticInterviewStatus"] = statisticInterviewStatus.Data;
                break;
            default:
                var statistic = new DashboardDtoGetInterviewStatus
                {
                    Accepted = 0,
                    Pending = 0,
                    Rejected = 0
                };
                statisticInterviewStatus.Data = statistic;
                ViewData["statisticInterviewStatus"] = statisticInterviewStatus.Data;
                break;
        }

        switch (statisticEmployees.Code)
        {
            case 200:
                ViewData["statisticEmployees"] = statisticEmployees.Data;
                break;
            default:
                var statistic = new DashboardsDtoGetStatus
                {
                    Idle = 0,
                    Working = 0
                };
                statisticEmployees.Data = statistic;
                ViewData["statisticEmployees"] = statisticEmployees.Data;
                break;
        }

        switch (grade.Code)
        {
            case 200:
                ViewData["grade"] = grade.Data;
                break;
            default:
                var gradeDtoGet = new GradeDtoGet
                {
                    GradeLevel = GradeEnum.B,
                    Guid = Guid.Empty,
                    ScoreSegment1 = 0,
                    ScoreSegment2 = 0,
                    ScoreSegment3 = 0,
                    ScoreSegment4 = 0,
                    TotalScore = 0
                };
                grade.Data = gradeDtoGet;
                ViewData["grade"] = grade.Data;
                break;
        }

        var top5Clients = await _dashboardRepository.GetTop5Clients();
        var dashboardDtoGetClient = new List<DashboardDtoGetClient>();
        if (top5Clients.Data is not null)
        {
            dashboardDtoGetClient = top5Clients.Data.ToList();
        }
        switch (top5Clients.Code)
        {
            case 200:
                ViewData["top5Clients"] = dashboardDtoGetClient;
                break;
            default:
                var top5ClientsDtoGet = new List<DashboardDtoGetClient>();
                ViewData["top5Clients"] = top5ClientsDtoGet;
                break;
        }

        return View();
    }
}
