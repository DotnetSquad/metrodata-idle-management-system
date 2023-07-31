using Client.Contracts;
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

        switch (statisticEmployees.Code)
        {
            case 200:
                ViewData["statisticEmployees"] = statisticEmployees.Data;
                break;
        }

        return View();
    }
}
