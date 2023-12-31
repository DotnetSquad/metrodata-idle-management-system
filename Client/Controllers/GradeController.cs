﻿using Client.Contracts;
using Client.DataTransferObjects.Employees;
using Client.DataTransferObjects.Grades;
using Client.Utilities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers;

public class GradeController : Controller
{
    public string isNotCollapsed = "GradeController";
    private readonly IGradeRepository _gradeRepository;
    private readonly IEmployeeRepository _employeeRepository;

    public GradeController(IGradeRepository gradeRepository, IEmployeeRepository employeeRepository)
    {
        _gradeRepository = gradeRepository;
        _employeeRepository = employeeRepository;
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

        var employees = await _employeeRepository.Get();
        var listEmployeeDtoGets = new List<EmployeeDtoGet>();

        if (employees.Data is not null) listEmployeeDtoGets = employees.Data.ToList();

        ViewData["Employees"] = listEmployeeDtoGets;
        ViewData["isNotCollapsed"] = isNotCollapsed;
        return View(ListGrades);
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.Trainer)}, {nameof(RoleLevelEnum.Admin)}")]
    [HttpGet]
    public IActionResult Create()
    {
        ViewData["isNotCollapsed"] = isNotCollapsed;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(GradeDtoGenerateScore gradeDtoPost)
    {
        var grade = await _gradeRepository.PostGenerate(gradeDtoPost);

        switch (grade.Code)
        {
            case 201:
                TempData["Success"] = grade.Message;
                return RedirectToAction(nameof(Index));
            case 400:
                TempData["Error"] = grade.Message;
                return RedirectToAction(nameof(Index));
            default:
                TempData["Error"] = "Failed to create grade.";
                return RedirectToAction(nameof(Index));
        }
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.Trainer)}, {nameof(RoleLevelEnum.Admin)}")]
    [HttpGet]
    public async Task<IActionResult> Update(Guid guid)
    {
        var grade = await _gradeRepository.Get(guid);
        var gradeDtoGet = new GradeDtoGet();

        ViewData["isNotCollapsed"] = isNotCollapsed;
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
                TempData["Error"] = "Grade not found.";
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
                TempData["Error"] = "Failed to update grade.";
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
                TempData["Error"] = "Failed to delete grade.";
                return RedirectToAction(nameof(Index));
        }
    }

    [HttpGet]
    public async Task<IActionResult> Details(Guid guid)
    {
        var employeeGuid = User.Claims.FirstOrDefault(x => x.Type == "Guid")?.Value;
        var employeeGuidTemp = Guid.Parse(employeeGuid);

        var grade = await _gradeRepository.Get(guid);
        var gradeDtoGet = new GradeDtoGet();

        var employee = await _employeeRepository.Get(employeeGuidTemp);
        var employeeDtoGets = new EmployeeDtoGet();

        if (employee.Data is not null)
        {
            employeeDtoGets = employee.Data;
        }

        ViewData["isNotCollapsed"] = isNotCollapsed;
        ViewData["Employee"] = employeeDtoGets;

        switch (grade.Code)
        {
            case 200:
                gradeDtoGet.Guid = grade.Data!.Guid;
                gradeDtoGet.GradeLevel = grade.Data!.GradeLevel;
                gradeDtoGet.ScoreSegment1 = grade.Data!.ScoreSegment1;
                gradeDtoGet.ScoreSegment2 = grade.Data!.ScoreSegment2;
                gradeDtoGet.ScoreSegment3 = grade.Data!.ScoreSegment3;
                gradeDtoGet.ScoreSegment4 = grade.Data!.ScoreSegment4;
                gradeDtoGet.TotalScore = grade.Data!.TotalScore;
                return View(gradeDtoGet);
            case 400:
                TempData["Error"] = grade.Message;
                return RedirectToAction(nameof(Index));
            default:
                TempData["Error"] = "Data grade not found.";
                return RedirectToAction(nameof(Index));
        }
    }
}


