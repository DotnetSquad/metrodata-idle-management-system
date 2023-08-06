using API.DataTransferObjects.EmployeeJobs;
using API.Services;
using API.Utilities.Enums;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]

public class EmployeeJobController : ControllerBase
{
    private readonly EmployeeJobService _employeeJobService;

    public EmployeeJobController(EmployeeJobService employeeJobService)
    {
        _employeeJobService = employeeJobService;
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.HR)}, {nameof(RoleLevelEnum.Admin)}, {nameof(RoleLevelEnum.Trainer)}, {nameof(RoleLevelEnum.Manager)}")]
    [HttpGet]
    public IActionResult Get()
    {
        var employeeJobs = _employeeJobService.Get();

        if (!employeeJobs.Any())
        {
            return NotFound(new ResponseHandler<EmployeeJobDtoGet>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "No employees job found",
                Data = null
            });
        }

        return Ok(new ResponseHandler<IEnumerable<EmployeeJobDtoGet>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Employee job found",
            Data = employeeJobs
        });
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.HR)}, {nameof(RoleLevelEnum.Admin)}, {nameof(RoleLevelEnum.Trainer)}, {nameof(RoleLevelEnum.Manager)}")]
    [HttpGet("{guid}")]
    public IActionResult Get(Guid guid)
    {
        var employeeJob = _employeeJobService.Get(guid);

        if (employeeJob is null)
        {
            return NotFound(new ResponseHandler<EmployeeJobDtoGet>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Employee job not found",
                Data = null
            });
        }

        return Ok(new ResponseHandler<EmployeeJobDtoGet>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Employee job found",
            Data = employeeJob
        });
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.Trainer)}, {nameof(RoleLevelEnum.Admin)}")]
    [HttpPost]
    public IActionResult Create(EmployeeJobDtoCreate employeeJobDtoCreate)
    {
        var employeeJobCreated = _employeeJobService.Create(employeeJobDtoCreate);

        if (employeeJobCreated is null)
        {
            return BadRequest(new ResponseHandler<EmployeeJobDtoCreate>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Employee job not created",
                Data = null
            });
        }

        return Ok(new ResponseHandler<EmployeeJobDtoCreate>
        {
            Code = StatusCodes.Status201Created,
            Status = HttpStatusCode.Created.ToString(),
            Message = "Employee job created",
            Data = employeeJobCreated
        });
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.Trainer)}, {nameof(RoleLevelEnum.Admin)}, {nameof(RoleLevelEnum.Manager)}, {nameof(RoleLevelEnum.HR)}")]
    [HttpPut]
    public IActionResult Update(EmployeeJobDtoUpdate employeeJobDtoUpdate)
    {
        var employeeJobUpdated = _employeeJobService.Update(employeeJobDtoUpdate);

        if (employeeJobUpdated == -1)
        {
            return NotFound(new ResponseHandler<EmployeeJobDtoUpdate>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Employee job not found",
                Data = null
            });
        }

        if (employeeJobUpdated == 0)
        {
            return BadRequest(new ResponseHandler<EmployeeJobDtoUpdate>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Employee job not updated",
                Data = null
            });
        }

        return Ok(new ResponseHandler<EmployeeJobDtoUpdate>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Employee job updated",
            Data = employeeJobDtoUpdate
        });
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.Trainer)}, {nameof(RoleLevelEnum.Admin)}")]
    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        var employeeJobDeleted = _employeeJobService.Delete(guid);

        if (employeeJobDeleted == -1)
        {
            return NotFound(new ResponseHandler<EmployeeJobDtoGet>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Employee job not found",
                Data = null
            });
        }

        if (employeeJobDeleted == 0)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseHandler<EmployeeJobDtoGet>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Employee job not deleted",
                Data = null
            });
        }

        return Ok(new ResponseHandler<EmployeeJobDtoGet>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Employee job deleted",
            Data = null
        });
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.HR)}, {nameof(RoleLevelEnum.Admin)}, {nameof(RoleLevelEnum.Trainer)}, {nameof(RoleLevelEnum.Manager)}")]
    [HttpGet("GetByJob/{guid}")]
    public IActionResult GetByJob(Guid guid)
    {
        var jobs = _employeeJobService.GetByJob(guid);

        if (!jobs.Any())
        {
            return NotFound(new ResponseHandler<EmployeeJobDtoGet>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "No projects found",
                Data = null
            });
        }

        return Ok(new ResponseHandler<IEnumerable<EmployeeJobDtoGet>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Projects found",
            Data = jobs
        });
    }
}
