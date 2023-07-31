using API.DataTransferObjects.Dashboards;
using API.Services;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DashboardController : ControllerBase
{
    private readonly DashboardService _dashboardService;

    public DashboardController(DashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    [HttpGet]
    public IActionResult GetEmployeeStatus()
    {
        var status = _dashboardService.GetEmployeeStatus();
        if (status is null)
        {
            return NotFound(new ResponseHandler<DashboardsDtoGetStatus>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "No accounts found",
                Data = null
            });
        }

        return Ok(new ResponseHandler<DashboardsDtoGetStatus>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "accounts found",
            Data = status
        });
    }
}
