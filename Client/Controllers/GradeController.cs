using Client.Contracts;
using Client.DataTransferObjects.Grades;
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
}
