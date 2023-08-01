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
    public async Task<IActionResult> Index()
    {
        var employeeProjects = await _employeeProjectRepository.Get();
        var listEmployeeProjectDtoGets = new List<EmployeeProjectDtoGet>();

        if (employeeProjects.Data is not null) listEmployeeProjectDtoGets = employeeProjects.Data.ToList();

        var employees = await _employeeRepository.Get();
        var listEmployeesDtoGets = new List<EmployeeDtoGet>();

        if (employees.Data != null) listEmployeesDtoGets = employees.Data.ToList();

        ViewData["Employees"] = listEmployeesDtoGets;

        var projects = await _projectRepository.Get();
        var listProjectsDtoGets = new List<ProjectDtoGet>();

        if (projects.Data != null) listProjectsDtoGets = projects.Data.ToList();

        ViewData["Projects"] = listProjectsDtoGets;

        return View(listEmployeeProjectDtoGets);
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.Trainer)}, {nameof(RoleLevelEnum.Admin)}")]
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var employees = await _employeeRepository.Get();
        var listEmployeeDtoGets = new List<EmployeeDtoGet>();

        if (employees.Data is not null) listEmployeeDtoGets = employees.Data.ToList();

        var project = await _projectRepository.Get();
        var listProjectDtoGets = new List<ProjectDtoGet>();

        if (project.Data is not null) listProjectDtoGets = project.Data.ToList();

        ViewData["Employees"] = listEmployeeDtoGets;
        ViewData["Projects"] = listProjectDtoGets;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(EmployeeProjectDtoGet employeeProjectDtoGet)
    {
        var employeeProject = await _employeeProjectRepository.Post(employeeProjectDtoGet);

        switch (employeeProject.Code)
        {
            case 201:
                TempData["Success"] = employeeProject.Message;
                return RedirectToAction(nameof(Index));
            case 400:
                TempData["Error"] = employeeProject.Message;
                return RedirectToAction(nameof(Index));
            default:
                TempData["Error"] = employeeProject.Message;
                return RedirectToAction(nameof(Index));
        }
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.Trainer)}, {nameof(RoleLevelEnum.Admin)}")]
    [HttpGet]
    public async Task<IActionResult> Update(Guid guid)
    {
        var employeeProject = await _employeeProjectRepository.Get(guid);
        var employeeProjectDtoGet = new EmployeeProjectDtoGet();

        if (employeeProject.Data is null)
        {
            TempData["Error"] = employeeProject.Message;
            return RedirectToAction(nameof(Index));
        }
        else
        {
            employeeProjectDtoGet.Guid = employeeProject.Data!.Guid;
            employeeProjectDtoGet.EmployeeGuid = employeeProject.Data!.EmployeeGuid;
            employeeProjectDtoGet.ProjectGuid = employeeProject.Data!.EmployeeGuid;
        }

        var employees = await _employeeRepository.Get();
        var listEmployeeDtoGets = new List<EmployeeDtoGet>();

        if (employees.Data is not null) listEmployeeDtoGets = employees.Data.ToList();

        var project = await _projectRepository.Get();
        var listProjectDtoGets = new List<ProjectDtoGet>();

        if (project.Data is not null) listProjectDtoGets = project.Data.ToList();

        ViewData["Employees"] = listEmployeeDtoGets;
        ViewData["Projects"] = listProjectDtoGets;

        return View(employeeProjectDtoGet);
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
                return RedirectToAction(nameof(Index));
            case 404:
                TempData["Error"] = employeeProject.Message;
                return RedirectToAction(nameof(Index));
            case 500:
                TempData["Error"] = employeeProject.Message;
                return RedirectToAction(nameof(Index));
            default:
                TempData["Error"] = employeeProject.Message;
                return RedirectToAction(nameof(Index));
        }
    }
}
