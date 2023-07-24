﻿using Microsoft.AspNetCore.Mvc;
using System.Net;
using API.DataTransferObjects.Jobs;
using API.Services;
using API.Utilities.Handlers;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JobController : ControllerBase
{
    private readonly JobService _jobService;

    public JobController(JobService jobService)
    {
        _jobService = jobService;
    }

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