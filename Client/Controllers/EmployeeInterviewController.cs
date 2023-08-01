using Client.Contracts;
using Client.DataTransferObjects.EmployeeInterviews;
using Client.DataTransferObjects.Employees;
using Client.DataTransferObjects.Interviews;
using Client.Utilities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers;

public class EmployeeInterviewController : Controller
{
    private readonly IEmployeeInterviewRepository _employeeInterviewRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IInterviewRepository _interviewRepository;

    public EmployeeInterviewController(IEmployeeInterviewRepository employeeInterviewRepository, IEmployeeRepository employeeRepository, IInterviewRepository interviewRepository)
    {
        _employeeInterviewRepository = employeeInterviewRepository;
        _employeeRepository = employeeRepository;
        _interviewRepository = interviewRepository;
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.HR)}, {nameof(RoleLevelEnum.Admin)}")]
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var employeeInterviews = await _employeeInterviewRepository.Get();
        var listEmployeeInterviewDtoGets = new List<EmployeeInterviewDtoGet>();

        if (employeeInterviews.Data is not null) listEmployeeInterviewDtoGets = employeeInterviews.Data.ToList();

        var employees = await _employeeRepository.Get();
        var listEmployeesDtoGets = new List<EmployeeDtoGet>();

        if (employees.Data != null) listEmployeesDtoGets = employees.Data.ToList();

        ViewData["Employees"] = listEmployeesDtoGets;

        var interviews = await _interviewRepository.Get();
        var listInterviewsDtoGets = new List<InterviewDtoGet>();

        if (interviews.Data != null) listInterviewsDtoGets = interviews.Data.ToList();

        ViewData["Interviews"] = listInterviewsDtoGets;

        return View(listEmployeeInterviewDtoGets);
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.HR)}, {nameof(RoleLevelEnum.Admin)}")]
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var employees = await _employeeRepository.Get();
        var listEmployeeDtoGets = new List<EmployeeDtoGet>();

        if (employees.Data is not null) listEmployeeDtoGets = employees.Data.ToList();

        var interviews = await _interviewRepository.Get();
        var listInterviewDtoGets = new List<InterviewDtoGet>();

        if (interviews.Data is not null) listInterviewDtoGets = interviews.Data.ToList();

        ViewData["Employees"] = listEmployeeDtoGets;
        ViewData["Interviews"] = listInterviewDtoGets;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(EmployeeInterviewDtoGet employeeInterviewDtoGet)
    {
        var employeeInterview = await _employeeInterviewRepository.Post(employeeInterviewDtoGet);

        switch (employeeInterview.Code)
        {
            case 201:
                TempData["Success"] = employeeInterview.Message;
                return RedirectToAction(nameof(Index));
            case 400:
                TempData["Error"] = employeeInterview.Message;
                return RedirectToAction(nameof(Index));
            default:
                TempData["Error"] = employeeInterview.Message;
                return RedirectToAction(nameof(Index));
        }
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.HR)}, {nameof(RoleLevelEnum.Admin)}")]
    [HttpGet]
    public async Task<IActionResult> Update(Guid guid)
    {
        var employeeInterview = await _employeeInterviewRepository.Get(guid);
        var employeeInterviewDtoGet = new EmployeeInterviewDtoGet();

        if (employeeInterview.Data is null)
        {
            TempData["Error"] = employeeInterview.Message;
            return RedirectToAction(nameof(Index));
        }
        else
        {
            employeeInterviewDtoGet.Guid = employeeInterview.Data!.Guid;
            employeeInterviewDtoGet.EmployeeGuid = employeeInterview.Data!.EmployeeGuid;
            employeeInterviewDtoGet.InterviewGuid = employeeInterview.Data!.InterviewGuid;
        }

        var employees = await _employeeRepository.Get();
        var listEmployeeDtoGets = new List<EmployeeDtoGet>();

        if (employees.Data is not null) listEmployeeDtoGets = employees.Data.ToList();

        var interviews = await _interviewRepository.Get();
        var listInterviewDtoGets = new List<InterviewDtoGet>();

        if (interviews.Data is not null) listInterviewDtoGets = interviews.Data.ToList();

        ViewData["Employees"] = listEmployeeDtoGets;
        ViewData["Interviews"] = listInterviewDtoGets;

        return View(employeeInterviewDtoGet);
    }

    [HttpPost]
    public async Task<IActionResult> Update(EmployeeInterviewDtoGet employeeInterviewDtoGet)
    {
        var employeeInterview = await _employeeInterviewRepository.Put(employeeInterviewDtoGet.Guid, employeeInterviewDtoGet);

        switch (employeeInterview.Code)
        {
            case 200:
                TempData["Success"] = employeeInterview.Message;
                return RedirectToAction(nameof(Index));
            case 400:
                TempData["Error"] = employeeInterview.Message;
                return RedirectToAction(nameof(Index));
            case 404:
                TempData["Error"] = employeeInterview.Message;
                return RedirectToAction(nameof(Index));
            default:
                TempData["Error"] = employeeInterview.Message;
                return RedirectToAction(nameof(Index));
        }
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.HR)}, {nameof(RoleLevelEnum.Admin)}")]
    [HttpPost]
    public async Task<IActionResult> Delete(Guid guid)
    {
        var employeeInterview = await _employeeInterviewRepository.Delete(guid);

        switch (employeeInterview.Code)
        {
            case 200:
                TempData["Success"] = employeeInterview.Message;
                return RedirectToAction(nameof(Index));
            case 404:
                TempData["Error"] = employeeInterview.Message;
                return RedirectToAction(nameof(Index));
            case 500:
                TempData["Error"] = employeeInterview.Message;
                return RedirectToAction(nameof(Index));
            default:
                TempData["Error"] = employeeInterview.Message;
                return RedirectToAction(nameof(Index));
        }
    }
}
