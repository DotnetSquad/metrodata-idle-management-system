using Client.Contracts;
using Client.DataTransferObjects.EmployeeJobs;
using Client.DataTransferObjects.Employees;
using Client.DataTransferObjects.Interviews;
using Client.DataTransferObjects.Jobs;
using Client.DataTransferObjects.Roles;
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
    private readonly IRoleRepository _roleRepository;

    public EmployeeJobController(IEmployeeRepository employeeRepository, IInterviewRepository interviewRepository, IEmployeeJobRepository employeeJobRepository, IJobRepository jobRepository, IRoleRepository roleRepository)
    {
        _employeeRepository = employeeRepository;
        _interviewRepository = interviewRepository;
        _employeeJobRepository = employeeJobRepository;
        _jobRepository = jobRepository;
        _roleRepository = roleRepository;
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.Trainer)}, {nameof(RoleLevelEnum.Manager)}, {nameof(RoleLevelEnum.Admin)}")]
    [HttpGet]
    public async Task<IActionResult> Index(Guid guid)
    {
        var employeeJobs = await _employeeJobRepository.GetByJob(guid);
        var listEmployeeJobDtoGets = new List<EmployeeJobDtoGet>();

        if (employeeJobs.Data is not null) listEmployeeJobDtoGets = employeeJobs.Data.ToList();

        var employees = await _employeeRepository.GetEmployeeByJob(guid);
        var listEmployeesDtoGets = new List<EmployeeDtoGet>();

        if (employees.Data != null) listEmployeesDtoGets = employees.Data.ToList();

        var interviews = await _interviewRepository.Get();
        var listInterviewsDtoGets = new List<InterviewDtoGet>();

        if (interviews.Data != null) listInterviewsDtoGets = interviews.Data.ToList();

        var jobs = await _jobRepository.Get();
        var listJobsDtoGets = new List<JobDtoGet>();

        if (jobs.Data != null) listJobsDtoGets = jobs.Data.ToList();

        ViewData["isNotCollapsed"] = isNotCollapsed;
        ViewData["EmployeeJobs"] = listEmployeeJobDtoGets;
        ViewData["Interviews"] = listInterviewsDtoGets;
        ViewData["Jobs"] = listJobsDtoGets;
        ViewData["JobGuid"] = guid;

        return View(listEmployeesDtoGets);
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.Admin)}, {nameof(RoleLevelEnum.Trainer)}")]
    [HttpGet]
    public async Task<IActionResult> Create(Guid guid)
    {
        var employeesExcludeJob = await _employeeRepository.GetExcludeJob(guid);
        var listEmployeeDtoGets = new List<EmployeeDtoGet>();

        if (employeesExcludeJob.Data is not null) listEmployeeDtoGets = employeesExcludeJob.Data.ToList();

        var roles = await _roleRepository.Get();
        var listRoleDtoGets = roles.Data?.ToList() ?? new List<RoleDtoGet>();

        var employeeRoleDtoGet = listRoleDtoGets.FirstOrDefault(role => role.Name == RoleLevelEnum.Employee.ToString());

        var filteredEmployees = new List<EmployeeDtoGet>();
        if (employeeRoleDtoGet != null)
        {
            var employeeRoles = await _employeeRepository.GetByRole(employeeRoleDtoGet.Guid);
            var listEmployeeRoleDtoGets = employeeRoles.Data?.ToList() ?? new List<EmployeeDtoGet>();

            filteredEmployees = listEmployeeDtoGets
                .Join(listEmployeeRoleDtoGets, emp => emp.Guid, empRole => empRole.Guid, (emp, empRole) => emp)
                .ToList();
        }

        ViewData["isNotCollapsed"] = isNotCollapsed;
        ViewData["Employees"] = filteredEmployees;
        ViewData["JobGuid"] = guid;
        return View();
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.Trainer)}, {nameof(RoleLevelEnum.Admin)}")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(EmployeeJobDtoGet employeeJobDtoGet)
    {
        var employeeJob = await _employeeJobRepository.Post(employeeJobDtoGet);
        ViewData["JobGuid"] = employeeJobDtoGet.JobGuid;

        if (employeeJob is null)
        {
            return RedirectToAction("Create", new { guid = employeeJobDtoGet.JobGuid });
        }
        switch (employeeJob.Code)
        {
            case 201:
                TempData["Success"] = employeeJob.Message;
                return RedirectToAction("Index", new { guid = employeeJobDtoGet.JobGuid });
            case 404:
                TempData["Error"] = employeeJob.Message;
                return RedirectToAction("Index", new { guid = employeeJobDtoGet.JobGuid });
            case 500:
                TempData["Error"] = employeeJob.Message;
                return RedirectToAction("Index", new { guid = employeeJobDtoGet.JobGuid });
            default:
                TempData["Error"] = "An error occurred, please try again later.";
                return RedirectToAction("Index", new { guid = employeeJobDtoGet.JobGuid });
        };
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

    [Authorize(Roles = $"{nameof(RoleLevelEnum.Trainer)}, {nameof(RoleLevelEnum.Admin)}")]
    [HttpPost]
    public async Task<IActionResult> Delete(Guid guid)
    {
        var getEmployeeJobt = await _employeeJobRepository.Get(guid);
        var employeeJob = await _employeeJobRepository.Delete(guid);

        switch (employeeJob.Code)
        {
            case 200:
                TempData["Success"] = employeeJob.Message;
                return RedirectToAction("Index", new { guid = getEmployeeJobt.Data.JobGuid });
            case 404:
                TempData["Error"] = employeeJob.Message;
                return RedirectToAction("Index", new { guid = getEmployeeJobt.Data.JobGuid });
            case 500:
                TempData["Error"] = employeeJob.Message;
                return RedirectToAction("Index", new { guid = getEmployeeJobt.Data.JobGuid });
            default:
                TempData["Error"] = "An error occurred, please try again later.";
                return RedirectToAction("Index", new { guid = getEmployeeJobt.Data.JobGuid });
        }
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.Manager)}, {nameof(RoleLevelEnum.Admin)}")]
    [HttpPost]
    public async Task<IActionResult> Approve(Guid guid)
    {
        var getEmployeeJob = await _employeeJobRepository.Get(guid);
        getEmployeeJob.Data.StatusApproval = StatusApprovalEnum.Accepted;
        var employeeJob = await _employeeJobRepository.Put(getEmployeeJob.Data.Guid, getEmployeeJob.Data);

        switch (employeeJob.Code)
        {
            case 200:
                TempData["Success"] = employeeJob.Message;
                return RedirectToAction("Index", new { guid = getEmployeeJob.Data.JobGuid });
            case 400:
                TempData["Error"] = employeeJob.Message;
                return RedirectToAction("Index", new { guid = getEmployeeJob.Data.JobGuid });
            case 404:
                TempData["Error"] = employeeJob.Message;
                return RedirectToAction("Index", new { guid = getEmployeeJob.Data.JobGuid });
            default:
                TempData["Error"] = employeeJob.Message;
                return RedirectToAction("Index", new { guid = getEmployeeJob.Data.JobGuid });
        }
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.Manager)}, {nameof(RoleLevelEnum.Admin)}")]
    [HttpPost]
    public async Task<IActionResult> Reject(Guid guid)
    {
        var getEmployeeJob = await _employeeJobRepository.Get(guid);
        getEmployeeJob.Data.StatusApproval = StatusApprovalEnum.Rejected;
        var employeeJob = await _employeeJobRepository.Put(getEmployeeJob.Data.Guid, getEmployeeJob.Data);

        switch (employeeJob.Code)
        {
            case 200:
                TempData["Success"] = employeeJob.Message;
                return RedirectToAction("Index", new { guid = getEmployeeJob.Data.JobGuid });
            case 400:
                TempData["Error"] = employeeJob.Message;
                return RedirectToAction("Index", new { guid = getEmployeeJob.Data.JobGuid });
            case 404:
                TempData["Error"] = employeeJob.Message;
                return RedirectToAction("Index", new { guid = getEmployeeJob.Data.JobGuid });
            default:
                TempData["Error"] = employeeJob.Message;
                return RedirectToAction("Index", new { guid = getEmployeeJob.Data.JobGuid });
        }
    }
}
