using Client.Contracts;
using Client.DataTransferObjects.Employees;
using Client.DataTransferObjects.Projects;
using Client.Utilities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers;

public class ProjectController : Controller
{
    private readonly IProjectRepository _projectRepository;
    private readonly IEmployeeRepository _employeeRepository;

    public ProjectController(IProjectRepository projectRepository, IEmployeeRepository employeeRepository)
    {
        _projectRepository = projectRepository;
        _employeeRepository = employeeRepository;
    }

    [Authorize(Roles =
        $"{nameof(RoleLevelEnum.Manager)}, {nameof(RoleLevelEnum.Trainer)}, {nameof(RoleLevelEnum.Admin)}")]
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var projects = await _projectRepository.Get();
        var listProjectDtoGets = new List<ProjectDtoGet>();

        if (projects.Data is not null)
        {
            listProjectDtoGets = projects.Data.ToList();
        }

        var employees = await _employeeRepository.Get();
        var listEmployeeDtoGets = new List<EmployeeDtoGet>();

        if (employees.Data is not null)
        {
            listEmployeeDtoGets = employees.Data.ToList();
        }

        ViewData["Employees"] = listEmployeeDtoGets;

        return View(listProjectDtoGets);
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.Manager)}, {nameof(RoleLevelEnum.Admin)}")]
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        // get employees
        var employees = await _employeeRepository.Get();
        var listEmployeeDtoGets = new List<EmployeeDtoGet>();

        if (employees.Data is not null)
        {
            listEmployeeDtoGets = employees.Data.ToList();
        }

        // add to view data
        ViewData["Employees"] = listEmployeeDtoGets;

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProjectDtoGet projectDtoPost)
    {
        var project = await _projectRepository.Post(projectDtoPost);

        switch (project.Code)
        {
            case 201:
                TempData["Success"] = project.Message;
                return RedirectToAction(nameof(Index));
            case 400:
                TempData["Error"] = project.Message;
                return RedirectToAction(nameof(Index));
            default:
                TempData["Error"] = "Failed to create project.";
                return RedirectToAction(nameof(Index));
        }
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.Manager)}, {nameof(RoleLevelEnum.Admin)}")]
    [HttpGet]
    public async Task<IActionResult> Update(Guid guid)
    {
        // get employees
        var employees = await _employeeRepository.Get();
        var listEmployeeDtoGets = new List<EmployeeDtoGet>();

        if (employees.Data != null)
        {
            listEmployeeDtoGets = employees.Data.ToList();
        }

        // add to view data
        ViewData["Employees"] = listEmployeeDtoGets;

        var project = await _projectRepository.Get(guid);
        var projectDtoGet = new ProjectDtoGet();

        if (project.Data is null)
        {
            TempData["Error"] = project.Message;
            return RedirectToAction(nameof(Index));
        }
        else
        {
            projectDtoGet.Guid = project.Data.Guid;
            projectDtoGet.NameProject = project.Data.NameProject;
            projectDtoGet.ProjectLead = project.Data.ProjectLead;
            projectDtoGet.Description = project.Data.Description;
        }

        return View(projectDtoGet);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(ProjectDtoGet projectDtoGet)
    {
        var project = await _projectRepository.Put(projectDtoGet.Guid, projectDtoGet);

        switch (project.Code)
        {
            case 200:
                TempData["Success"] = project.Message;
                return RedirectToAction(nameof(Index));
            case 400:
                TempData["Error"] = project.Message;
                return RedirectToAction(nameof(Index));
            case 404:
                TempData["Error"] = project.Message;
                return RedirectToAction(nameof(Index));
            default:
                TempData["Error"] = "Failed to update project.";
                return RedirectToAction(nameof(Index));
        }
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.Manager)}, {nameof(RoleLevelEnum.Admin)}")]
    [HttpPost]
    public async Task<IActionResult> Delete(Guid guid)
    {
        var project = await _projectRepository.Delete(guid);

        switch (project.Code)
        {
            case 200:
                TempData["Success"] = project.Message;
                return RedirectToAction(nameof(Index));
            case 404:
                TempData["Error"] = project.Message;
                return RedirectToAction(nameof(Index));
            case 500:
                TempData["Error"] = project.Message;
                return RedirectToAction(nameof(Index));
            default:
                TempData["Error"] = "Failed to delete project.";
                return RedirectToAction(nameof(Index));
        }
    }
}
