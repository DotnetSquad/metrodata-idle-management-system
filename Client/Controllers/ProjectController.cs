using Client.Contracts;
using Client.DataTransferObjects.Employees;
using Client.DataTransferObjects.Projects;
using Client.DataTransferObjects.Roles;
using Client.Repositories;
using Microsoft.AspNetCore.Mvc;public class ProjectController : Controller
{
    private readonly IProjectRepository _repository;
    private readonly IEmployeeRepository _employeeRepository;

    public ProjectController(IProjectRepository repository, IEmployeeRepository employeeRepository)
    {
        _repository = repository;
        _employeeRepository = employeeRepository;
    }

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
}
