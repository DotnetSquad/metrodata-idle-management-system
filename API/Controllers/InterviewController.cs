using API.DataTransferObjects.Interviews;
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
public class InterviewController : ControllerBase
{
    private readonly InterviewService _interviewService;

    public InterviewController(InterviewService interviewService)
    {
        _interviewService = interviewService;
    }

    [Authorize(Roles = $"{nameof(RoleLevel.HR)}, {nameof(RoleLevel.Manager)}, {nameof(RoleLevel.Trainer)}")]
    [HttpGet]
    public IActionResult Get()
    {
        var interviews = _interviewService.Get();

        if (!interviews.Any())
        {
            return NotFound(new ResponseHandler<InterviewDtoGet>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "No interview found",
                Data = null
            });
        }

        return Ok(new ResponseHandler<IEnumerable<InterviewDtoGet>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Interviews found",
            Data = interviews
        });
    }

    [Authorize(Roles = $"{nameof(RoleLevel.Employee)}")]
    [HttpGet("{guid}")]
    public IActionResult Get(Guid guid)
    {
        var interview = _interviewService.Get(guid);

        if (interview is null)
        {
            return NotFound(new ResponseHandler<InterviewDtoGet>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Interview not found",
                Data = null
            });
        }

        return Ok(new ResponseHandler<InterviewDtoGet>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Interview found",
            Data = interview
        });
    }

    [Authorize(Roles = $"{nameof(RoleLevel.HR)}")]
    [HttpPost]
    public IActionResult Create(InterviewDtoCreate interviewDtoCreate)
    {
        var interviewCreated = _interviewService.Create(interviewDtoCreate);

        if (interviewCreated is null)
        {
            return BadRequest(new ResponseHandler<InterviewDtoCreate>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Interview not created",
                Data = null
            });
        }

        return Ok(new ResponseHandler<InterviewDtoCreate>
        {
            Code = StatusCodes.Status201Created,
            Status = HttpStatusCode.Created.ToString(),
            Message = "Interview created",
            Data = interviewCreated
        });
    }

    [Authorize(Roles = $"{nameof(RoleLevel.HR)}")]
    [HttpPut]
    public IActionResult Update(InterviewDtoUpdate interviewDtoUpdate)
    {
        var interviewUpdated = _interviewService.Update(interviewDtoUpdate);

        if (interviewUpdated == -1)
        {
            return NotFound(new ResponseHandler<InterviewDtoUpdate>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Interview not found",
                Data = null
            });
        }

        if (interviewUpdated == 0)
        {
            return BadRequest(new ResponseHandler<InterviewDtoUpdate>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Interview not updated",
                Data = null
            });
        }

        return Ok(new ResponseHandler<InterviewDtoUpdate>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Placement updated",
            Data = interviewDtoUpdate
        });
    }

    [Authorize(Roles = $"{nameof(RoleLevel.HR)}")]
    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        var interviewDeleted = _interviewService.Delete(guid);

        if (interviewDeleted == -1)
        {
            return NotFound(new ResponseHandler<InterviewDtoGet>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Interview not found",
                Data = null
            });
        }

        if (interviewDeleted == 0)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseHandler<InterviewDtoGet>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Interview not deleted",
                Data = null
            });
        }

        return Ok(new ResponseHandler<InterviewDtoGet>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Interview deleted",
            Data = null
        });
    }
}
