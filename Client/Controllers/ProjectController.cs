using Client.Contracts;
using Client.DataTransferObjects.Employees;
using Client.DataTransferObjects.Projects;
using Client.DataTransferObjects.Roles;
using Client.Utilities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers;

public class ProjectController : Controller
{
    public string isNotCollapsed = "ProjectController";
    private readonly IProjectRepository _projectRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IRoleRepository _roleRepository;

    public ProjectController(IProjectRepository projectRepository, IEmployeeRepository employeeRepository, IRoleRepository roleRepository)
    {
        _projectRepository = projectRepository;
        _employeeRepository = employeeRepository;
        _roleRepository = roleRepository;
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

        if (User.IsInRole(RoleLevelEnum.Trainer.ToString()))
        {
            isNotCollapsed = "EmployeeProjectController";
        }

        ViewData["isNotCollapsed"] = isNotCollapsed;
        ViewData["Employees"] = listEmployeeDtoGets;

        return View(listProjectDtoGets);
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.Manager)}, {nameof(RoleLevelEnum.Admin)}")]
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var roles = await _roleRepository.Get();
        var listRoleDtoGets = new List<RoleDtoGet>();

        if (roles.Data is not null) listRoleDtoGets = roles.Data.ToList();
        var roleDtoGet = new RoleDtoGet();

        foreach (var role in listRoleDtoGets)
        {
            if (role.Name == RoleLevelEnum.Trainer.ToString())
            {
                roleDtoGet = role;
            }
        }

        var trainers = await _employeeRepository.GetByRole(roleDtoGet.Guid);
        var listTrainerDtoGets = new List<EmployeeDtoGet>();

        if (trainers.Data is not null) listTrainerDtoGets = trainers.Data.ToList();

        if (User.IsInRole(RoleLevelEnum.Trainer.ToString()))
        {
            isNotCollapsed = "EmployeeProjectController";
        }

        // add to view data
        ViewData["isNotCollapsed"] = isNotCollapsed;
        ViewData["Employees"] = listTrainerDtoGets;

        return View();
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.Manager)}, {nameof(RoleLevelEnum.Admin)}")]
    [HttpPost]
    [ValidateAntiForgeryToken]
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
        var roles = await _roleRepository.Get();
        var listRoleDtoGets = new List<RoleDtoGet>();

        if (roles.Data is not null) listRoleDtoGets = roles.Data.ToList();
        var roleDtoGet = new RoleDtoGet();

        foreach (var role in listRoleDtoGets)
            if (role.Name == RoleLevelEnum.Trainer.ToString())
                roleDtoGet = role;

        var trainers = await _employeeRepository.GetByRole(roleDtoGet.Guid);
        var listEmployeeDtoGets = new List<EmployeeDtoGet>();

        if (trainers.Data is not null)
            listEmployeeDtoGets = trainers.Data.ToList();

        if (User.IsInRole(RoleLevelEnum.Trainer.ToString()))
            isNotCollapsed = "EmployeeProjectController";

        ViewData["isNotCollapsed"] = isNotCollapsed;
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

    [Authorize(Roles = $"{nameof(RoleLevelEnum.Manager)}, {nameof(RoleLevelEnum.Admin)}")]
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
