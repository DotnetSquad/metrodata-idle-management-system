using API.DataTransferObjects.Jobs;
using API.Services;
using API.Utilities.Enums;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
/*[Authorize(Roles = $"{nameof(RoleLevel.HR)}")]*/
public class JobController : ControllerBase
{
    private readonly JobService _jobService;

    public JobController(JobService jobService)
    {
        _jobService = jobService;
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.HR)}, {nameof(RoleLevelEnum.Admin)}, {nameof(RoleLevelEnum.Manager)}, {nameof(RoleLevelEnum.Trainer)}")]
    [HttpGet]
    public IActionResult Get()
    {
        var jobs = _jobService.Get();

        if (!jobs.Any())
        {
            return NotFound(new ResponseHandler<JobDtoGet>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "No jobs found",
                Data = null
            });
        }

        return Ok(new ResponseHandler<IEnumerable<JobDtoGet>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Jobs found",
            Data = jobs
        });
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.HR)}, {nameof(RoleLevelEnum.Admin)}")]
    [HttpGet("{guid}")]
    public IActionResult Get(Guid guid)
    {
        var job = _jobService.Get(guid);

        if (job is null)
        {
            return NotFound(new ResponseHandler<JobDtoGet>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Job not found",
                Data = null
            });
        }

        return Ok(new ResponseHandler<JobDtoGet>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Job found",
            Data = job
        });
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.HR)}, {nameof(RoleLevelEnum.Admin)}")]
    [HttpPost]
    public IActionResult Create(JobDtoCreate jobDtoCreate)
    {
        var jobCreated = _jobService.Create(jobDtoCreate);

        if (jobCreated is null)
        {
            return BadRequest(new ResponseHandler<JobDtoCreate>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Job not created",
                Data = null
            });
        }

        return Ok(new ResponseHandler<JobDtoCreate>
        {
            Code = StatusCodes.Status201Created,
            Status = HttpStatusCode.Created.ToString(),
            Message = "Job created",
            Data = jobCreated
        });
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.HR)}, {nameof(RoleLevelEnum.Admin)}")]
    [HttpPut]
    public IActionResult Update(JobDtoUpdate jobDtoUpdate)
    {
        var jobUpdated = _jobService.Update(jobDtoUpdate);

        if (jobUpdated == -1)
        {
            return NotFound(new ResponseHandler<JobDtoUpdate>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Job not found",
                Data = null
            });
        }

        if (jobUpdated == 0)
        {
            return BadRequest(new ResponseHandler<JobDtoUpdate>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Job not updated",
                Data = null
            });
        }

        return Ok(new ResponseHandler<JobDtoUpdate>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Job updated",
            Data = jobDtoUpdate
        });
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.HR)}, {nameof(RoleLevelEnum.Admin)}")]
    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        var jobDeleted = _jobService.Delete(guid);

        if (jobDeleted == -1)
        {
            return NotFound(new ResponseHandler<JobDtoGet>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Job not found",
                Data = null
            });
        }

        if (jobDeleted == 0)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseHandler<JobDtoGet>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Job not deleted",
                Data = null
            });
        }

        return Ok(new ResponseHandler<JobDtoGet>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Job deleted",
            Data = null
        });
    }
}
