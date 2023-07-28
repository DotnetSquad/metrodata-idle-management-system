using Client.Contracts;
using Client.DataTransferObjects.Grades;
using Client.Utilities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers;

public class GradeController : Controller
{
    private readonly IGradeRepository _repository;

    public GradeController(IGradeRepository repository)
    {
        _repository = repository;
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.Trainer)}, {nameof(RoleLevelEnum.Manager)}")]
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var result = await _repository.Get();
        var ListGrade = new List<GradeDtoGet>();

        if (result.Data != null)
        {
            result.Data.ToList().ForEach(x => x.TotalScore = (int)x.TotalScore);
            ListGrade = result.Data.ToList();
        }

        return View(ListGrade);
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.Trainer)}")]
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(GradeDtoGenerateScore gradeDtoPost)
    {
        var result = await _repository.PostGenerate(gradeDtoPost);
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

    [Authorize(Roles = $"{nameof(RoleLevelEnum.Trainer)}")]
    [HttpGet]
    public async Task<IActionResult> Update(Guid guid)
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
    public async Task<IActionResult> Update(GradeDtoGet grade)
    {
        if (ModelState.IsValid)
        {
            var result = await _repository.PutGenerate(grade.Guid, grade);
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

    [Authorize(Roles = $"{nameof(RoleLevelEnum.Trainer)}")]
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
