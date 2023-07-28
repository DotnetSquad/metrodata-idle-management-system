using Client.Contracts;
using Client.DataTransferObjects.Employees;
using Client.Utilities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers;

public class EmployeeController : Controller
{
    private readonly IEmployeeRepository _repository;
    private readonly IGradeRepository _gradeRepository;
    private readonly IProfileRepository _profileRepository;

    public EmployeeController(IEmployeeRepository repository, IGradeRepository gradeRepository,
        IProfileRepository profileRepository)
    {
        _repository = repository;
        _gradeRepository = gradeRepository;
        _profileRepository = profileRepository;
    }

    [Authorize(Roles =
        $"{nameof(RoleLevelEnum.HR)}, {nameof(RoleLevelEnum.Manager)}, {nameof(RoleLevelEnum.Trainer)}, {nameof(RoleLevelEnum.Admin)}")]
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

    [Authorize(Roles = $"{nameof(RoleLevelEnum.HR)}, {nameof(RoleLevelEnum.Admin)}")]
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

    [Authorize(Roles = $"{nameof(RoleLevelEnum.HR)}, {nameof(RoleLevelEnum.Admin)}")]
    [HttpGet]
    public async Task<IActionResult> Update(Guid guid)
    {
        var result = await _repository.Get(guid);

        var employee = new EmployeeDtoGet();
        if (result.Data?.Guid is null)
        {
            return View(employee);
        }
        else
        {
            employee.Guid = result.Data.Guid;
            employee.Nik = result.Data.Nik;
            employee.FirstName = result.Data.FirstName;
            employee.LastName = result.Data.LastName;
            employee.BirthDate = result.Data.BirthDate;
            employee.Gender = result.Data.Gender;
            employee.HiringDate = result.Data.HiringDate;
            employee.Email = result.Data.Email;
            employee.PhoneNumber = result.Data.PhoneNumber;
            employee.Status = result.Data.Status;
            employee.GradeGuid = result.Data.GradeGuid;
            employee.ProfileGuid = result.Data.ProfileGuid;
        }

        return View(employee);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(EmployeeDtoGet employeeDtoGet)
    {
        if (ModelState.IsValid)
        {
            var result = await _repository.Put(employeeDtoGet.Guid, employeeDtoGet);
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

    [Authorize(Roles = $"{nameof(RoleLevelEnum.HR)}, {nameof(RoleLevelEnum.Admin)}")]
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
