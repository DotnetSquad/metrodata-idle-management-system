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
                Message = "No employees status found",
                Data = null
            });
        }

        return Ok(new ResponseHandler<DashboardsDtoGetStatus>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Employees status found",
            Data = status
        });
    }

    [HttpGet("GetInterviewStatus")]
    public IActionResult GetInterviewStatus()
    {
        var status = _dashboardService.GetInterviewStatus();
        if (status is null)
        {
            return NotFound(new ResponseHandler<DashboardDtoGetInterviewStatus>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "No interview status found",
                Data = null
            });
        }

        return Ok(new ResponseHandler<DashboardDtoGetInterviewStatus>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Interview status found",
            Data = status
        });
    }

    [HttpGet("GetStatus")]
    public IActionResult GetProjectStatus()
    {
        var status = _dashboardService.GetStatus();
        if (status is null)
        {
            return NotFound(new ResponseHandler<DashboardDtoGetInterviewStatus>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "No status found",
                Data = null
            });
        }

        return Ok(new ResponseHandler<DashboardDtoGetInterviewStatus>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Status found",
            Data = status
        });
    }

    [HttpGet("GetTop5Client")]
    public IActionResult GetTop5Client()
    {
        var clients = _dashboardService.GetTop5Client();
        if (!clients.Any())
        {
            return NotFound(new ResponseHandler<DashboardDtoGetClient>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "No client found",
                Data = null
            });
        }

        return Ok(new ResponseHandler<IEnumerable<DashboardDtoGetClient>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Clients found",
            Data = clients
        });
    }
}
