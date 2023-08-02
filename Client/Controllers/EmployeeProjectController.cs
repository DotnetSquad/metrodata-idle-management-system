using Client.Contracts;
using Client.DataTransferObjects.EmployeeProjects;
using Client.DataTransferObjects.Employees;
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

    [Authorize(Roles = $"{nameof(RoleLevelEnum.Trainer)}, {nameof(RoleLevelEnum.Admin)}")]
    [HttpGet]
    public async Task<IActionResult> Index(Guid guid)
    {
        var employeeProjects = await _employeeProjectRepository.Get();
        var listEmployeeProjectDtoGets = new List<EmployeeProjectDtoGet>();

        if (employeeProjects.Data is not null) listEmployeeProjectDtoGets = employeeProjects.Data.Where(ep => ep.ProjectGuid == guid).ToList();

        var employees = await _employeeRepository.GetByProject(guid);
        var listEmployees = new List<EmployeeDtoGet>();

        if (employees.Data is not null) listEmployees = employees.Data.ToList();

        ViewData["EmployeeProjects"] = listEmployeeProjectDtoGets;
        ViewData["ProjectGuid"] = guid;

        var projects = await _projectRepository.Get();
        var listProjectsDtoGets = new List<ProjectDtoGet>();

        if (projects.Data != null) listProjectsDtoGets = projects.Data.ToList();

        ViewData["isNotCollapsed"] = isNotCollapsed;
        ViewData["Projects"] = listProjectsDtoGets;

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


        ViewData["isNotCollapsed"] = isNotCollapsed;
        ViewData["Employees"] = listEmployeeDtoGets;
        ViewData["ProjectGuid"] = guid;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(EmployeeProjectDtoGet employeeProjectDtoGet)
    {
        var employeeProject = await _employeeProjectRepository.Post(employeeProjectDtoGet);

        if (employeeProject is null)
        {
            return RedirectToAction("Create", new { guid = employeeProjectDtoGet.ProjectGuid });
        }
        
        ViewData["isNotCollapsed"] = isNotCollapsed;
        return RedirectToAction("Index", new { guid = employeeProjectDtoGet.ProjectGuid });
    }

    [HttpPost]
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
        var employeeProject = await _employeeProjectRepository.Delete(guid);

        switch (employeeProject.Code)
        {
            case 200:
                TempData["Success"] = employeeProject.Message;
                return RedirectToAction("Index", "Project");
            case 404:
                TempData["Error"] = employeeProject.Message;
                return RedirectToAction("Index", "Project");
            case 500:
                TempData["Error"] = employeeProject.Message;
                return RedirectToAction("Index", "Project");
            default:
                TempData["Error"] = "An error occurred, please try again later.";
                return RedirectToAction("Index", "Project");
        }
    }
}
