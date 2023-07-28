using API.DataTransferObjects.EmployeeInterviews;
using API.Services;
using API.Utilities.Enums;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = $"{nameof(RoleLevelEnum.HR)}, {nameof(RoleLevelEnum.Admin)}")]
public class EmployeeInterviewController : ControllerBase
{
    private readonly EmployeeInterviewService _employeeInterviewService;

    public EmployeeInterviewController(EmployeeInterviewService employeeInterviewService)
    {
        _employeeInterviewService = employeeInterviewService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var employeeInterviews = _employeeInterviewService.Get();

        if (!employeeInterviews.Any())
        {
            return NotFound(new ResponseHandler<EmployeeInterviewDtoGet>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "No employees found",
                Data = null
            });
        }

        return Ok(employeeInterviews);
    }

    [HttpGet("{guid}")]
    public IActionResult Get(Guid guid)
    {
        var employeeInterview = _employeeInterviewService.Get(guid);

        if (employeeInterview is null)
        {
            return NotFound(new ResponseHandler<EmployeeInterviewDtoGet>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Employee not found",
                Data = null
            });
        }

        return Ok(employeeInterview);
    }

    [HttpPost]
    public IActionResult Create(EmployeeInterviewDtoCreate employeeInterviewDtoCreate)
    {
        var employeeInterviewCreated = _employeeInterviewService.Create(employeeInterviewDtoCreate);

        if (employeeInterviewCreated is null)
        {
            return BadRequest(new ResponseHandler<EmployeeInterviewDtoCreate>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Employee not created",
                Data = null
            });
        }

        return Ok(new ResponseHandler<EmployeeInterviewDtoCreate>
        {
            Code = StatusCodes.Status201Created,
            Status = HttpStatusCode.Created.ToString(),
            Message = "Employee created",
            Data = employeeInterviewCreated
        });
    }

    [HttpPut]
    public IActionResult Update(EmployeeInterviewDtoUpdate employeeInterviewDtoUpdate)
    {
        var employeeInterviewUpdated = _employeeInterviewService.Update(employeeInterviewDtoUpdate);

        if (employeeInterviewUpdated == -1)
        {
            return NotFound(new ResponseHandler<EmployeeInterviewDtoUpdate>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Employee not found",
                Data = null
            });
        }

        if (employeeInterviewUpdated == 0)
        {
            return BadRequest(new ResponseHandler<EmployeeInterviewDtoUpdate>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Employee not updated",
                Data = null
            });
        }

        return Ok(new ResponseHandler<EmployeeInterviewDtoUpdate>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Employee updated",
            Data = employeeInterviewDtoUpdate
        });
    }

    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        var employeeInterviewDeleted = _employeeInterviewService.Delete(guid);

        if (employeeInterviewDeleted == -1)
        {
            return NotFound(new ResponseHandler<EmployeeInterviewDtoGet>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Employee not found",
                Data = null
            });
        }

        if (employeeInterviewDeleted == 0)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseHandler<EmployeeInterviewDtoGet>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Role not deleted",
                Data = null
            });
        }

        return Ok(new ResponseHandler<EmployeeInterviewDtoGet>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Employee deleted",
            Data = null
        });
    }
}
