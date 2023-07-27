using API.DataTransferObjects.Placements;
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
public class PlacementController : ControllerBase
{
    private readonly PlacementService _placementService;

    public PlacementController(PlacementService placementService)
    {
        _placementService = placementService;
    }

    [Authorize(Roles = $"{nameof(RoleLevel.HR)}, {nameof(RoleLevel.Manager)}, {nameof(RoleLevel.Trainer)}")]
    [HttpGet]
    public IActionResult Get()
    {
        var placements = _placementService.Get();

        if (!placements.Any())
        {
            return NotFound(new ResponseHandler<PlacementDtoGet>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "No placements found",
                Data = null
            });
        }

        return Ok(new ResponseHandler<IEnumerable<PlacementDtoGet>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Placements found",
            Data = placements
        });
    }

    [Authorize(Roles = $"{nameof(RoleLevel.Employee)}")]
    [HttpGet("{guid}")]
    public IActionResult Get(Guid guid)
    {
        var placement = _placementService.Get(guid);

        if (placement is null)
        {
            return NotFound(new ResponseHandler<PlacementDtoGet>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Placement not found",
                Data = null
            });
        }

        return Ok(new ResponseHandler<PlacementDtoGet>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Placement found",
            Data = placement
        });
    }

    [Authorize(Roles = $"{nameof(RoleLevel.HR)}")]
    [HttpPost]
    public IActionResult Create(PlacementDtoCreate placementDtoCreate)
    {
        var placementCreated = _placementService.Create(placementDtoCreate);

        if (placementCreated is null)
        {
            return BadRequest(new ResponseHandler<PlacementDtoCreate>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Placement not created",
                Data = null
            });
        }

        return Ok(new ResponseHandler<PlacementDtoCreate>
        {
            Code = StatusCodes.Status201Created,
            Status = HttpStatusCode.Created.ToString(),
            Message = "Placement created",
            Data = placementCreated
        });
    }

    [Authorize(Roles = $"{nameof(RoleLevel.HR)}")]
    [HttpPut]
    public IActionResult Update(PlacementDtoUpdate placementDtoUpdate)
    {
        var placementUpdated = _placementService.Update(placementDtoUpdate);

        if (placementUpdated == -1)
        {
            return NotFound(new ResponseHandler<PlacementDtoUpdate>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Placement not found",
                Data = null
            });
        }

        if (placementUpdated == 0)
        {
            return BadRequest(new ResponseHandler<PlacementDtoUpdate>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Placement not updated",
                Data = null
            });
        }

        return Ok(new ResponseHandler<PlacementDtoUpdate>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Placement updated",
            Data = placementDtoUpdate
        });
    }

    [Authorize(Roles = $"{nameof(RoleLevel.HR)}")]
    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        var placementDeleted = _placementService.Delete(guid);

        if (placementDeleted == -1)
        {
            return NotFound(new ResponseHandler<PlacementDtoGet>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Placement not found",
                Data = null
            });
        }

        if (placementDeleted == 0)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseHandler<PlacementDtoGet>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Placement not deleted",
                Data = null
            });
        }

        return Ok(new ResponseHandler<PlacementDtoGet>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Placement deleted",
            Data = null
        });
    }
}
