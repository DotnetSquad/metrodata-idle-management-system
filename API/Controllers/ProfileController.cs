using API.DataTransferObjects.Profiles;
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
public class ProfileController : ControllerBase
{
    private readonly ProfileService _profileService;

    public ProfileController(ProfileService profileService)
    {
        _profileService = profileService;
    }

    [Authorize(Roles =
        $"{nameof(RoleLevelEnum.HR)}, {nameof(RoleLevelEnum.Manager)}, {nameof(RoleLevelEnum.Trainer)}, {nameof(RoleLevelEnum.Admin)}")]
    [HttpGet]
    public IActionResult Get()
    {
        var profiles = _profileService.Get();

        if (!profiles.Any())
        {
            return NotFound(new ResponseHandler<ProfileDtoGet>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "No profiles found",
                Data = null
            });
        }

        return Ok(new ResponseHandler<IEnumerable<ProfileDtoGet>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Profiles found",
            Data = profiles
        });
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.Employee)}, {nameof(RoleLevelEnum.Admin)}, {nameof(RoleLevelEnum.HR)}, {nameof(RoleLevelEnum.Manager)}, {nameof(RoleLevelEnum.Trainer)}")]
    [HttpGet("{guid}")]
    public IActionResult Get(Guid guid)
    {
        var profile = _profileService.Get(guid);

        if (profile is null)
        {
            return NotFound(new ResponseHandler<ProfileDtoGet>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Profile not found",
                Data = null
            });
        }

        return Ok(new ResponseHandler<ProfileDtoGet>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Profile found",
            Data = profile
        });
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.Employee)}, {nameof(RoleLevelEnum.Admin)}")]
    [HttpPost]
    public IActionResult Create(ProfileDtoCreate profileDtoCreate)
    {
        var profileCreated = _profileService.Create(profileDtoCreate);

        if (profileCreated is null)
        {
            return BadRequest(new ResponseHandler<ProfileDtoCreate>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Profile not created",
                Data = null
            });
        }

        return Ok(new ResponseHandler<ProfileDtoCreate>
        {
            Code = StatusCodes.Status201Created,
            Status = HttpStatusCode.Created.ToString(),
            Message = "Profile created",
            Data = profileCreated
        });
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.Employee)}, {nameof(RoleLevelEnum.Admin)},{nameof(RoleLevelEnum.HR)}, {nameof(RoleLevelEnum.Manager)}, {nameof(RoleLevelEnum.Trainer)}")]
    [HttpPut]
    public IActionResult Update(ProfileDtoUpdate profileDtoUpdate)
    {
        var profileUpdated = _profileService.Update(profileDtoUpdate);

        if (profileUpdated == -1)
        {
            return NotFound(new ResponseHandler<ProfileDtoUpdate>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Profile not found",
                Data = null
            });
        }

        if (profileUpdated == 0)
        {
            return BadRequest(new ResponseHandler<ProfileDtoUpdate>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Profile not updated",
                Data = null
            });
        }

        return Ok(new ResponseHandler<ProfileDtoUpdate>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Profile updated",
            Data = profileDtoUpdate
        });
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.HR)}, {nameof(RoleLevelEnum.Admin)}")]
    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        var profileDeleted = _profileService.Delete(guid);

        if (profileDeleted == -1)
        {
            return NotFound(new ResponseHandler<ProfileDtoGet>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Profile not found",
                Data = null
            });
        }

        if (profileDeleted == 0)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseHandler<ProfileDtoGet>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Profile not deleted",
                Data = null
            });
        }

        return Ok(new ResponseHandler<ProfileDtoGet>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Profile deleted",
            Data = null
        });
    }
}
