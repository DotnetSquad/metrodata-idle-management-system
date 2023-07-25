using Client.Contracts;
using Client.DataTransferObjects.Grades;
using Client.DataTransferObjects.Roles;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers;

public class GradeController : Controller
{
    private readonly IGradeRepository _repository;

    public GradeController(IGradeRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var result = await _repository.Get();
        var ListGrade = new List<GradeDtoGet>();

        if (result.Data != null)
        {
            ListGrade = result.Data.ToList();
        }
        return View(ListGrade);
    }

    // create 
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(GradeDtoGet gradeDtoPost)
    {
        var result = await _repository.Post(gradeDtoPost);
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
