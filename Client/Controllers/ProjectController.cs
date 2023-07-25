using Client.Contracts;
using Client.DataTransferObjects.Projects;
using Microsoft.AspNetCore.Mvc;public class ProjectController : Controller
{
    private readonly IProjectRepository _repository;

    public ProjectController(IProjectRepository repository)
    {
        _repository = repository;
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
}
