using Client.Contracts;
using Client.DataTransferObjects.Employees;
using Client.DataTransferObjects.Projects;
using Client.Utilities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers;

public class ProjectController : Controller
{
    private readonly IProjectRepository _repository;
    private readonly IEmployeeRepository _employeeRepository;

    public ProjectController(IProjectRepository repository, IEmployeeRepository employeeRepository)
    {
        _repository = repository;
        _employeeRepository = employeeRepository;
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.Manager)}, {nameof(RoleLevelEnum.Trainer)}")]
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var result = await _repository.Get();
        var ListProject = new List<ProjectDtoGet>();

        if (result.Data != null)
        {
            ListProject = result.Data.ToList();
        }
        return View(ListProject);
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.Manager)}")]
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        // get employees
        var result = await _employeeRepository.Get();
        var listEmployeeDtoGets = new List<EmployeeDtoGet>();

        if (result.Data != null)
        {
            listEmployeeDtoGets = result.Data.ToList();
        }

        // add to view data
        ViewData["Employees"] = listEmployeeDtoGets;

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProjectDtoGet projectDtoPost)
    {
        var result = await _repository.Post(projectDtoPost);
        if (result.Status == "200")
        {
            TempData["Success"] = "Data success created";
            return RedirectToAction(nameof(Index));
        }
        else if (result.Status == "409")
        {
            ModelState.AddModelError(string.Empty, result.Message);
            return View();
        }
        return RedirectToAction(nameof(Index));
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.Manager)}")]
    [HttpGet]
    public async Task<IActionResult> Update(Guid guid)
    {
        // get employees
        var result = await _employeeRepository.Get();
        var listEmployeeDtoGets = new List<EmployeeDtoGet>();

        if (result.Data != null)
        {
            listEmployeeDtoGets = result.Data.ToList();
        }

        // add to view data
        ViewData["Employees"] = listEmployeeDtoGets;

        var value = await _repository.Get(guid);
        var project = new ProjectDtoGet();
        if (value.Data?.Guid is null)
        {
            return View(project);
        }
        else
        {
            project.Guid = value.Data.Guid;
            project.NameProject = value.Data.NameProject;
            project.ProjectLead = value.Data.ProjectLead;
            project.Description = value.Data.Description;
        }

        return View(project);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(ProjectDtoGet project)
    {
        if (ModelState.IsValid)
        {
            var result = await _repository.Put(project.Guid, project);
            if (result.Code == 200)
            {
                return RedirectToAction(nameof(Index));
            }
            else if (result.Status == "409")
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View();
            }
        }
        return View();
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.Manager)}")]
    [HttpPost]
    public async Task<IActionResult> Delete(Guid guid)
    {
        var result = await _repository.Delete(guid);
        if (result.Code == 200)
        {
            return RedirectToAction(nameof(Index));
        }
        return RedirectToAction(nameof(Index));
    }
}
