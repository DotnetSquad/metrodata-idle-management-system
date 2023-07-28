using API.DataTransferObjects.Companies;
using API.Services;
using API.Utilities.Enums;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = $"{nameof(RoleLevelEnum.HR)}")]
public class CompanyController : ControllerBase
{
    private readonly CompanyService _companyService;

    public CompanyController(CompanyService companyService)
    {
        _companyService = companyService;
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.Employee)}")]
    [HttpGet]
    public IActionResult Get()
    {
        var companies = _companyService.Get();

        if (!companies.Any())
        {
            return NotFound(new ResponseHandler<CompanyDtoGet>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "No companies found",
                Data = null
            });
        }

        return Ok(new ResponseHandler<IEnumerable<CompanyDtoGet>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Companies found",
            Data = companies
        });
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.Employee)}")]
    [HttpGet("{guid}")]
    public IActionResult Get(Guid guid)
    {
        var company = _companyService.Get(guid);

        if (company is null)
        {
            return NotFound(new ResponseHandler<CompanyDtoGet>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Company not found",
                Data = null
            });
        }

        return Ok(new ResponseHandler<CompanyDtoGet>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Company found",
            Data = company
        });
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.HR)}")]
    [HttpPost]
    public IActionResult Create(CompanyDtoCreate companyDtoCreate)
    {
        var companyCreated = _companyService.Create(companyDtoCreate);

        if (companyCreated is null)
        {
            return BadRequest(new ResponseHandler<CompanyDtoCreate>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Company not created",
                Data = null
            });
        }

        return Ok(new ResponseHandler<CompanyDtoCreate>
        {
            Code = StatusCodes.Status201Created,
            Status = HttpStatusCode.Created.ToString(),
            Message = "Company created",
            Data = companyCreated
        });
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.HR)}")]
    [HttpPut]
    public IActionResult Update(CompanyDtoUpdate companyDtoUpdate)
    {
        var companyUpdated = _companyService.Update(companyDtoUpdate);

        if (companyUpdated == -1)
        {
            return NotFound(new ResponseHandler<CompanyDtoUpdate>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Company not found",
                Data = null
            });
        }

        if (companyUpdated == 0)
        {
            return BadRequest(new ResponseHandler<CompanyDtoUpdate>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Company not updated",
                Data = null
            });
        }

        return Ok(new ResponseHandler<CompanyDtoUpdate>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Company updated",
            Data = companyDtoUpdate
        });
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.HR)}")]
    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        var companyDeleted = _companyService.Delete(guid);

        if (companyDeleted == -1)
        {
            return NotFound(new ResponseHandler<CompanyDtoGet>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Company not found",
                Data = null
            });
        }

        if (companyDeleted == 0)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseHandler<CompanyDtoGet>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Company not deleted",
                Data = null
            });
        }

        return Ok(new ResponseHandler<CompanyDtoGet>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Company deleted",
            Data = null
        });
    }
}
