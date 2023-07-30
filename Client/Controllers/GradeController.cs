using Client.Contracts;
using Client.DataTransferObjects.Grades;
using Client.Utilities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers;

public class GradeController : Controller
{
    private readonly IGradeRepository _gradeRepository;

    public GradeController(IGradeRepository gradeRepository)
    {
        _gradeRepository = gradeRepository;
    }

    [Authorize(Roles =
        $"{nameof(RoleLevelEnum.Trainer)}, {nameof(RoleLevelEnum.Manager)}, {nameof(RoleLevelEnum.Admin)}")]
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var grades = await _gradeRepository.Get();
        var ListGrades = new List<GradeDtoGet>();

        if (grades.Data != null)
        {
            grades.Data.ToList().ForEach(x => x.TotalScore = (int)x.TotalScore);
            ListGrades = grades.Data.ToList();
        }

        return View(ListGrades);
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.Trainer)}, {nameof(RoleLevelEnum.Admin)}")]
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(GradeDtoGenerateScore gradeDtoPost)
    {
        var grade = await _gradeRepository.PostGenerate(gradeDtoPost);

        switch (grade.Code)
        {
            case 200:
                TempData["Success"] = grade.Message;
                return RedirectToAction(nameof(Index));
            case 400:
                TempData["Error"] = grade.Message;
                return RedirectToAction(nameof(Index));
            default:
                TempData["Error"] = grade.Message;
                return RedirectToAction(nameof(Index));
        }
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.Trainer)}, {nameof(RoleLevelEnum.Admin)}")]
    [HttpGet]
    public async Task<IActionResult> Update(Guid guid)
    {
        var grade = await _gradeRepository.Get(guid);
        var gradeDtoGet = new GradeDtoGet();

        switch (grade.Code)
        {
            case 200:
                gradeDtoGet.Guid = grade.Data.Guid;
                gradeDtoGet.GradeLevel = grade.Data.GradeLevel;
                gradeDtoGet.ScoreSegment1 = grade.Data.ScoreSegment1;
                gradeDtoGet.ScoreSegment2 = grade.Data.ScoreSegment2;
                gradeDtoGet.ScoreSegment3 = grade.Data.ScoreSegment3;
                gradeDtoGet.ScoreSegment4 = grade.Data.ScoreSegment4;
                gradeDtoGet.TotalScore = grade.Data.TotalScore;
                return View(gradeDtoGet);
            case 400:
                TempData["Error"] = grade.Message;
                return RedirectToAction(nameof(Index));
            default:
                TempData["Error"] = grade.Message;
                return RedirectToAction(nameof(Index));
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(Guid id, GradeDtoGet gradeDtoGet)
    {
        var grade = await _gradeRepository.PutGenerate(gradeDtoGet.Guid, gradeDtoGet);

        switch (grade.Code)
        {
            case 200:
                TempData["Success"] = grade.Message;
                return RedirectToAction(nameof(Index));
            case 400:
                TempData["Error"] = grade.Message;
                return RedirectToAction(nameof(Index));
            case 404:
                TempData["Error"] = grade.Message;
                return RedirectToAction(nameof(Index));
            default:
                TempData["Error"] = grade.Message;
                return RedirectToAction(nameof(Index));
        }
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.Trainer)}, {nameof(RoleLevelEnum.Admin)}")]
    [HttpPost]
    public async Task<IActionResult> Delete(Guid guid)
    {
        var grade = await _gradeRepository.Delete(guid);

        switch (grade.Code)
        {
            case 200:
                TempData["Success"] = grade.Message;
                return RedirectToAction(nameof(Index));
            case 500:
                TempData["Error"] = grade.Message;
                return RedirectToAction(nameof(Index));
            case 404:
                TempData["Error"] = grade.Message;
                return RedirectToAction(nameof(Index));
            default:
                TempData["Error"] = grade.Message;
                return RedirectToAction(nameof(Index));
        }
    }
}


