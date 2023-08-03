using Client.Contracts;
using Client.DataTransferObjects.EmployeeProjects;
using Client.DataTransferObjects.Employees;
using Client.DataTransferObjects.Projects;
using Client.Utilities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers;

public class EmployeeProjectController : Controller
{
    public string isNotCollapsed = "EmployeeProjectController";
    private readonly IEmployeeProjectRepository _employeeProjectRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IProjectRepository _projectRepository;

    public EmployeeProjectController(IEmployeeProjectRepository employeeProjectRepository, IEmployeeRepository employeeRepository, IProjectRepository projectRepository)
    {
        _employeeProjectRepository = employeeProjectRepository;
        _employeeRepository = employeeRepository;
        _projectRepository = projectRepository;
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.Trainer)}, {nameof(RoleLevelEnum.Manager)}, {nameof(RoleLevelEnum.Admin)}")]
    [HttpGet]
    public async Task<IActionResult> Index(Guid guid)
    {
        var employeeProjects = await _employeeProjectRepository.GetByProject(guid);
        var listEmployeeProjectDtoGets = new List<EmployeeProjectDtoGet>();

        if (employeeProjects.Data is not null) listEmployeeProjectDtoGets = employeeProjects.Data.ToList();

        var employees = await _employeeRepository.GetByProject(guid);
        var listEmployees = new List<EmployeeDtoGet>();

        if (employees.Data is not null) listEmployees = employees.Data.ToList();

        var project = await _projectRepository.Get();
        var listProjectDtoGets = new List<ProjectDtoGet>();

        if (project.Data is not null) listProjectDtoGets = project.Data.ToList();


        ViewData["Projects"] = listProjectDtoGets;
        ViewData["EmployeeProjects"] = listEmployeeProjectDtoGets;
        ViewData["ProjectGuid"] = guid;
        ViewData["isNotCollapsed"] = isNotCollapsed;

        return View(listEmployees);
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.Trainer)}, {nameof(RoleLevelEnum.Admin)}")]
    [HttpGet]
    public async Task<IActionResult> Create(Guid guid)
    {
        var employeesExcludeProject = await _employeeRepository.GetExcludeProject(guid);
        var project = await _projectRepository.Get(guid);
        var listEmployeeDtoGets = new List<EmployeeDtoGet>();

        if (employeesExcludeProject.Data is not null) listEmployeeDtoGets = employeesExcludeProject.Data.ToList();

        ViewData["Employees"] = listEmployeeDtoGets;
        ViewData["ProjectGuid"] = guid;
        ViewData["isNotCollapsed"] = isNotCollapsed;

        return View();
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.Trainer)}, {nameof(RoleLevelEnum.Admin)}")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(EmployeeProjectDtoGet employeeProjectDtoGet)
    {
        var employeeProject = await _employeeProjectRepository.Post(employeeProjectDtoGet);
        ViewData["ProjectGuid"] = employeeProjectDtoGet.ProjectGuid;

        if (employeeProject is null)
        {
            return RedirectToAction("Create", new { guid = employeeProjectDtoGet.ProjectGuid });
        }
        switch (employeeProject.Code)
        {
            case 201:
                TempData["Success"] = employeeProject.Message;
                return RedirectToAction("Index", new { guid = employeeProjectDtoGet.ProjectGuid });
            case 404:
                TempData["Error"] = employeeProject.Message;
                return RedirectToAction("Index", new { guid = employeeProjectDtoGet.ProjectGuid });
            case 500:
                TempData["Error"] = employeeProject.Message;
                return RedirectToAction("Index", new { guid = employeeProjectDtoGet.ProjectGuid });
            default:
                TempData["Error"] = "An error occurred, please try again later.";
                return RedirectToAction("Index", new { guid = employeeProjectDtoGet.ProjectGuid });
        };
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.Trainer)}, {nameof(RoleLevelEnum.Admin)}")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(EmployeeProjectDtoGet employeeProjectDtoGet)
    {
        var employeeProject = await _employeeProjectRepository.Put(employeeProjectDtoGet.Guid, employeeProjectDtoGet);

        switch (employeeProject.Code)
        {
            case 200:
                TempData["Success"] = employeeProject.Message;
                return RedirectToAction(nameof(Index));
            case 400:
                TempData["Error"] = employeeProject.Message;
                return RedirectToAction(nameof(Index));
            case 404:
                TempData["Error"] = employeeProject.Message;
                return RedirectToAction(nameof(Index));
            default:
                TempData["Error"] = employeeProject.Message;
                return RedirectToAction(nameof(Index));
        }
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.Trainer)}, {nameof(RoleLevelEnum.Admin)}")]
    [HttpPost]
    public async Task<IActionResult> Delete(Guid guid)
    {
        var getEmployeeProject = await _employeeProjectRepository.Get(guid);
        var employeeProject = await _employeeProjectRepository.Delete(guid);

        switch (employeeProject.Code)
        {
            case 200:
                TempData["Success"] = employeeProject.Message;
                return RedirectToAction("Index", new { guid = getEmployeeProject.Data.ProjectGuid });
            case 404:
                TempData["Error"] = employeeProject.Message;
                return RedirectToAction("Index", new { guid = getEmployeeProject.Data.ProjectGuid });
            case 500:
                TempData["Error"] = employeeProject.Message;
                return RedirectToAction("Index", new { guid = getEmployeeProject.Data.ProjectGuid });
            default:
                TempData["Error"] = "An error occurred, please try again later.";
                return RedirectToAction("Index", new { guid = getEmployeeProject.Data.ProjectGuid });
        }
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.Manager)}, {nameof(RoleLevelEnum.Admin)}")]
    [HttpPost]
    public async Task<IActionResult> Approve(Guid guid)
    {
        var getEmployeeProject = await _employeeProjectRepository.Get(guid);
        getEmployeeProject.Data.StatusApproval = StatusApprovalEnum.Accepted;
        var employeeProject = await _employeeProjectRepository.Put(getEmployeeProject.Data.Guid, getEmployeeProject.Data);

        switch (employeeProject.Code)
        {
            case 200:
                TempData["Success"] = employeeProject.Message;
                return RedirectToAction("Index", new { guid = getEmployeeProject.Data.ProjectGuid });
            case 400:
                TempData["Error"] = employeeProject.Message;
                return RedirectToAction("Index", new { guid = getEmployeeProject.Data.ProjectGuid });
            case 404:
                TempData["Error"] = employeeProject.Message;
                return RedirectToAction("Index", new { guid = getEmployeeProject.Data.ProjectGuid });
            default:
                TempData["Error"] = employeeProject.Message;
                return RedirectToAction("Index", new { guid = getEmployeeProject.Data.ProjectGuid });
        }
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.Manager)}, {nameof(RoleLevelEnum.Admin)}")]
    [HttpPost]
    public async Task<IActionResult> Reject(Guid guid)
    {
        var getEmployeeProject = await _employeeProjectRepository.Get(guid);
        getEmployeeProject.Data.StatusApproval = StatusApprovalEnum.Rejected;
        var employeeProject = await _employeeProjectRepository.Put(getEmployeeProject.Data.Guid, getEmployeeProject.Data);

        switch (employeeProject.Code)
        {
            case 200:
                TempData["Success"] = employeeProject.Message;
                return RedirectToAction("Index", new { guid = getEmployeeProject.Data.ProjectGuid });
            case 400:
                TempData["Error"] = employeeProject.Message;
                return RedirectToAction("Index", new { guid = getEmployeeProject.Data.ProjectGuid });
            case 404:
                TempData["Error"] = employeeProject.Message;
                return RedirectToAction("Index", new { guid = getEmployeeProject.Data.ProjectGuid });
            default:
                TempData["Error"] = employeeProject.Message;
                return RedirectToAction("Index", new { guid = getEmployeeProject.Data.ProjectGuid });
        }
    }
}
