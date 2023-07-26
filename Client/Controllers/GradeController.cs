using Client.Contracts;
using Client.DataTransferObjects.Grades;
using Client.DataTransferObjects.Roles;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

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

    [HttpGet]
    public async Task<IActionResult> Edit(Guid guid)
    {
        var result = await _repository.Get(guid);
        var grade = new GradeDtoGet();
        if (result.Data?.Guid is null)
        {
            return View(grade);
        }
        else
        {
            grade.Guid = result.Data.Guid;
            grade.GradeLevel = result.Data.GradeLevel;
            grade.ScoreSegment1 = result.Data.ScoreSegment1;
            grade.ScoreSegment2 = result.Data.ScoreSegment2;
            grade.ScoreSegment3 = result.Data.ScoreSegment3;
            grade.ScoreSegment4 = result.Data.ScoreSegment4;
            grade.TotalScore = result.Data.TotalScore;
        }

        return View(grade);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(GradeDtoGet grade)
    {
        if (ModelState.IsValid)
        {
            var result = await _repository.Put(grade.Guid, grade);
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
}
