using Client.Contracts;
using Client.DataTransferObjects.Dashboards;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers;

[Authorize]
public class DashboardController : Controller
{
    private readonly IDashboardRepository _dashboardRepository;

    public DashboardController(IDashboardRepository dashboardRepository)
    {
        _dashboardRepository = dashboardRepository;
    }

    public async Task<IActionResult> Index()
    {
        var statisticEmployees = await _dashboardRepository.GetStatisticEmployee();
        var statisticInterviewStatus = await _dashboardRepository.GetStatisticInterviewStatus();

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

        return View();
    }
}
