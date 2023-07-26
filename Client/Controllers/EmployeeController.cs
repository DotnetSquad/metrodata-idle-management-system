using Client.Contracts;
using Client.DataTransferObjects.Employees;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers;

public class EmployeeController : Controller
{
    private readonly IEmployeeRepository _repository;
    private readonly IGradeRepository _gradeRepository;
    private readonly IProfileRepository _profileRepository;

    public EmployeeController(IEmployeeRepository repository, IGradeRepository gradeRepository, IProfileRepository profileRepository)
    {
        _repository = repository;
        _gradeRepository = gradeRepository;
        _profileRepository = profileRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var result = await _repository.Get();
        var ListEmployee = new List<EmployeeDtoGet>();

        if (result.Data != null)
        {
            ListEmployee = result.Data.ToList();
        }
        return View(ListEmployee);
    }

    // create 
    [HttpGet]
    public IActionResult Create()
    {
        /*// get grade
        var resultGrade = await _gradeRepository.Get();
        var listGradeDtoGets = new List<GradeDtoGet>();

        if (resultGrade.Data != null)
        {
            listGradeDtoGets = resultGrade.Data.ToList();
        }

        // get profile
        var resultProfile = await _profileRepository.Get();
        var listProfileDtoGets = new List<ProfileDtoGet>();

        if (resultProfile.Data != null)
        {
            listProfileDtoGets = resultProfile.Data.ToList();
        }

        // add to view data
        ViewData["Grades"] = listGradeDtoGets;
        ViewData["Profiles"] = listProfileDtoGets;*/

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(EmployeeDtoGet employeeDtoPost)
    {
        var result = await _repository.Post(employeeDtoPost);
        if (result.Status == "201")
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
