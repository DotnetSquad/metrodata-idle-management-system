using System.Net;
using API.DataTransferObjects.Employees;
using API.Services;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly EmployeeService _employeeService;
    
    public EmployeeController(EmployeeService employeeService)
    {
        _employeeService = employeeService;
    }
    
    [HttpGet]
    public IActionResult Get()
    {
        var employees = _employeeService.Get();

        if (!employees.Any())
        {
            return NotFound(new ResponseHandler<EmployeeDtoGet>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "No employees found",
                Data = null
            });
        }
        
        return Ok(new ResponseHandler<IEnumerable<EmployeeDtoGet>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Employees found",
            Data = employees
        });
    }
    
    [HttpGet("{guid}")]
    public IActionResult Get(Guid guid)
    {
        var employee = _employeeService.Get(guid);

        if (employee is null)
        {
            return NotFound(new ResponseHandler<EmployeeDtoGet>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Employee not found",
                Data = null
            });
        }
        
        return Ok(new ResponseHandler<EmployeeDtoGet>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Employee found",
            Data = employee
        });
    }
    
    [HttpPost]
    public IActionResult Create(EmployeeDtoCreate employeeDtoCreate)
    {
        var employeeCreated = _employeeService.Create(employeeDtoCreate);

        if (employeeCreated is null)
        {
            return BadRequest(new ResponseHandler<EmployeeDtoCreate>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Employee not created",
                Data = null
            });
        }
        
        return Ok(new ResponseHandler<EmployeeDtoCreate>
        {
            Code = StatusCodes.Status201Created,
            Status = HttpStatusCode.Created.ToString(),
            Message = "Employee created",
            Data = employeeCreated
        });
    }
    
    [HttpPut]
    public IActionResult Update(EmployeeDtoUpdate employeeDtoUpdate)
    {
        var employeeUpdated = _employeeService.Update(employeeDtoUpdate);

        if (employeeUpdated == -1)
        {
            return NotFound(new ResponseHandler<EmployeeDtoUpdate>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Employee not found",
                Data = null
            });
        }
        
        if (employeeUpdated == 0)
        {
            return BadRequest(new ResponseHandler<EmployeeDtoUpdate>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Employee not updated",
                Data = null
            });
        }
        
        return Ok(new ResponseHandler<EmployeeDtoUpdate>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Employee updated",
            Data = employeeDtoUpdate
        });
    }
    
    [HttpDelete("{guid}")]
    public IActionResult Delete(Guid guid)
    {
        var employeeDeleted = _employeeService.Delete(guid);

        if (employeeDeleted == -1)
        {
            return NotFound(new ResponseHandler<EmployeeDtoGet>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Employee not found",
                Data = null
            });
        }
        
        if (employeeDeleted == 0)
        {
            return BadRequest(new ResponseHandler<EmployeeDtoGet>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Employee not deleted",
                Data = null
            });
        }
        
        return Ok(new ResponseHandler<EmployeeDtoGet>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Employee deleted",
            Data = null
        });
    }
}
