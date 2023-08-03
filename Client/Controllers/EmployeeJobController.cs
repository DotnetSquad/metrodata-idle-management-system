using Client.Contracts;
using Client.DataTransferObjects.EmployeeJobs;
using Client.DataTransferObjects.Employees;
using Client.DataTransferObjects.Interviews;
using Client.DataTransferObjects.Jobs;
using Client.Utilities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers;

public class EmployeeJobController : Controller
{
    public string isNotCollapsed = "EmployeeInterviewController";
    private readonly IEmployeeJobRepository _employeeJobRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IInterviewRepository _interviewRepository;
    private readonly IJobRepository _jobRepository;

    public EmployeeJobController(IEmployeeRepository employeeRepository, IInterviewRepository interviewRepository, IEmployeeJobRepository employeeJobRepository, IJobRepository jobRepository)
    {
        _employeeRepository = employeeRepository;
        _interviewRepository = interviewRepository;
        _employeeJobRepository = employeeJobRepository;
        _jobRepository = jobRepository;
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.HR)}, {nameof(RoleLevelEnum.Admin)}")]
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var employeeJobs = await _employeeJobRepository.Get();
        var listEmployeeJobDtoGets = new List<EmployeeJobDtoGet>();

        if (employeeJobs.Data is not null) listEmployeeJobDtoGets = employeeJobs.Data.ToList();

        var employees = await _employeeRepository.Get();
        var listEmployeesDtoGets = new List<EmployeeDtoGet>();

        if (employees.Data != null) listEmployeesDtoGets = employees.Data.ToList();

        ViewData["Employees"] = listEmployeesDtoGets;

        var interviews = await _interviewRepository.Get();
        var listInterviewsDtoGets = new List<InterviewDtoGet>();

        if (interviews.Data != null) listInterviewsDtoGets = interviews.Data.ToList();

        var jobs = await _jobRepository.Get();
        var listJobsDtoGets = new List<JobDtoGet>();

        if (jobs.Data != null) listJobsDtoGets = jobs.Data.ToList();

        ViewData["isNotCollapsed"] = isNotCollapsed;
        ViewData["Interviews"] = listInterviewsDtoGets;
        ViewData["Jobs"] = listJobsDtoGets;

        return View(listEmployeeJobDtoGets);
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

        var jobs = await _jobRepository.Get();
        var listJobsDtoGets = new List<JobDtoGet>();

        if (jobs.Data != null) listJobsDtoGets = jobs.Data.ToList();

        ViewData["isNotCollapsed"] = isNotCollapsed;
        ViewData["Employees"] = listEmployeeDtoGets;
        ViewData["Interviews"] = listInterviewDtoGets;
        ViewData["Jobs"] = listJobsDtoGets;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(EmployeeJobDtoGet employeeJobDtoGet)
    {
        var employeeJob = await _employeeJobRepository.Post(employeeJobDtoGet);

        switch (employeeJob.Code)
        {
            case 201:
                TempData["Success"] = employeeJob.Message;
                return RedirectToAction(nameof(Index));
            case 400:
                TempData["Error"] = employeeJob.Message;
                return RedirectToAction(nameof(Index));
            default:
                TempData["Error"] = employeeJob.Message;
                return RedirectToAction(nameof(Index));
        }
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.HR)}, {nameof(RoleLevelEnum.Admin)}")]
    [HttpGet]
    public async Task<IActionResult> Update(Guid guid)
    {
        var employeeJob = await _employeeJobRepository.Get(guid);
        var employeeJobDtoGet = new EmployeeJobDtoGet();

        if (employeeJob.Data is null)
        {
            TempData["Error"] = employeeJob.Message;
            return RedirectToAction(nameof(Index));
        }
        else
        {
            employeeJobDtoGet.Guid = employeeJob.Data!.Guid;
            employeeJobDtoGet.EmployeeGuid = employeeJob.Data!.EmployeeGuid;
            employeeJobDtoGet.InterviewGuid = employeeJob.Data!.InterviewGuid;
            employeeJobDtoGet.JobGuid = employeeJob.Data!.JobGuid;
            employeeJobDtoGet.StatusApproval = employeeJob.Data!.StatusApproval;
        }

        var employees = await _employeeRepository.Get();
        var listEmployeeDtoGets = new List<EmployeeDtoGet>();

        if (employees.Data is not null) listEmployeeDtoGets = employees.Data.ToList();

        var interviews = await _interviewRepository.Get();
        var listInterviewDtoGets = new List<InterviewDtoGet>();

        if (interviews.Data is not null) listInterviewDtoGets = interviews.Data.ToList();

        var jobs = await _jobRepository.Get();
        var listJobsDtoGets = new List<JobDtoGet>();

        if (jobs.Data != null) listJobsDtoGets = jobs.Data.ToList();

        ViewData["isNotCollapsed"] = isNotCollapsed;
        ViewData["Employees"] = listEmployeeDtoGets;
        ViewData["Interviews"] = listInterviewDtoGets;
        ViewData["Jobs"] = listJobsDtoGets;

        return View(employeeJobDtoGet);
    }

    [HttpPost]
    public async Task<IActionResult> Update(EmployeeJobDtoGet employeeJobDtoGet)
    {
        var employeeJob = await _employeeJobRepository.Put(employeeJobDtoGet.Guid, employeeJobDtoGet);

        switch (employeeJob.Code)
        {
            case 200:
                TempData["Success"] = employeeJob.Message;
                return RedirectToAction(nameof(Index));
            case 400:
                TempData["Error"] = employeeJob.Message;
                return RedirectToAction(nameof(Index));
            case 404:
                TempData["Error"] = employeeJob.Message;
                return RedirectToAction(nameof(Index));
            default:
                TempData["Error"] = employeeJob.Message;
                return RedirectToAction(nameof(Index));
        }
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.HR)}, {nameof(RoleLevelEnum.Admin)}")]
    [HttpPost]
    public async Task<IActionResult> Delete(Guid guid)
    {
        var employeeJob = await _employeeJobRepository.Delete(guid);

        switch (employeeJob.Code)
        {
            case 200:
                TempData["Success"] = employeeJob.Message;
                return RedirectToAction(nameof(Index));
            case 404:
                TempData["Error"] = employeeJob.Message;
                return RedirectToAction(nameof(Index));
            case 500:
                TempData["Error"] = employeeJob.Message;
                return RedirectToAction(nameof(Index));
            default:
                TempData["Error"] = employeeJob.Message;
                return RedirectToAction(nameof(Index));
        }
    }
}
