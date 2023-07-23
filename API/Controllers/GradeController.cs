using Microsoft.AspNetCore.Mvc;
using System.Net;
using API.DataTransferObjects.Grades;
using API.Services;
using API.Utilities.Handlers;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GradeController : ControllerBase
{
    private readonly GradeService _gradeService;

    public GradeController(GradeService gradeService)
    {
        _gradeService = gradeService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var grades = _gradeService.Get();

        if (!grades.Any())
        {
            return NotFound(new ResponseHandler<GradeDtoGet>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "No grades found",
                Data = null
            });
        }

        return Ok(new ResponseHandler<IEnumerable<GradeDtoGet>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Grades found",
            Data = grades
        });
    }

    [HttpGet("{guid}")]
    public IActionResult Get(Guid guid)
    {
        var grade = _gradeService.Get(guid);

        if (grade is null)
        {
            return NotFound(new ResponseHandler<GradeDtoGet>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Grade not found",
                Data = null
            });
        }

        return Ok(new ResponseHandler<GradeDtoGet>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Grade found",
            Data = grade
        });
    }

    [HttpPost]
    public IActionResult Create(GradeDtoCreate gradeDtoCreate)
    {
        var gradeCreated = _gradeService.Create(gradeDtoCreate);

        if (gradeCreated is null)
        {
            return BadRequest(new ResponseHandler<GradeDtoCreate>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Grade not created",
                Data = null
            });
        }

        return Ok(new ResponseHandler<GradeDtoCreate>
        {
            Code = StatusCodes.Status201Created,
            Status = HttpStatusCode.Created.ToString(),
            Message = "Grade created",
            Data = gradeCreated
        });
    }

    [HttpPut]
    public IActionResult Update(GradeDtoUpdate gradeDtoUpdate)
    {
        var gradeUpdated = _gradeService.Update(gradeDtoUpdate);

        if (gradeUpdated == -1)
        {
            return NotFound(new ResponseHandler<GradeDtoUpdate>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Grade not found",
                Data = null
            });
        }

        if (gradeUpdated == 0)
        {
            return BadRequest(new ResponseHandler<GradeDtoUpdate>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Grade not updated",
                Data = null
            });
        }

        return Ok(new ResponseHandler<GradeDtoUpdate>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Grade updated",
            Data = gradeDtoUpdate
        });
    }

    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        var gradeDeleted = _gradeService.Delete(guid);

        if (gradeDeleted == -1)
        {
            return NotFound(new ResponseHandler<GradeDtoGet>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Grade not found",
                Data = null
            });
        }

        if (gradeDeleted == 0)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseHandler<GradeDtoGet>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Grade not deleted",
                Data = null
            });
        }

        return Ok(new ResponseHandler<GradeDtoGet>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Grade deleted",
            Data = null
        });
    }
}
