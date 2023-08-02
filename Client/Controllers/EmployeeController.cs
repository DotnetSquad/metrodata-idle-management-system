using Client.Contracts;
using Client.DataTransferObjects.Employees;
using Client.Utilities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers;

public class EmployeeController : Controller
{
    public string isNotCollapsed = "EmployeeController";
    private readonly IEmployeeRepository _employeerepository;
    private readonly IGradeRepository _gradeRepository;
    private readonly IProfileRepository _profileRepository;

    public EmployeeController(IEmployeeRepository employeerepository, IGradeRepository gradeRepository,
        IProfileRepository profileRepository)
    {
        _employeerepository = employeerepository;
        _gradeRepository = gradeRepository;
        _profileRepository = profileRepository;
    }

    [Authorize(Roles =
        $"{nameof(RoleLevelEnum.HR)}, {nameof(RoleLevelEnum.Manager)}, {nameof(RoleLevelEnum.Trainer)}, {nameof(RoleLevelEnum.Admin)}")]
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var emplooyees = await _employeerepository.Get();
        var listEmployees = new List<EmployeeDtoGet>();

        if (emplooyees.Data is not null)
        {
            listEmployees = emplooyees.Data.ToList();
        }

        ViewData["isNotCollapsed"] = isNotCollapsed;
        return View(listEmployees);
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

        ViewData["isNotCollapsed"] = isNotCollapsed;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(EmployeeDtoGet employeeDtoPost)
    {
        var employee = await _employeerepository.Post(employeeDtoPost);
        switch (employee.Code)
        {
            case 201:
                TempData["Success"] = employee.Message;
                return RedirectToAction(nameof(Index));
            case 400:
                TempData["Error"] = employee.Message;
                return RedirectToAction(nameof(Index));
            default:
                TempData["Error"] = "Failed to create employee.";
                return RedirectToAction(nameof(Index));
        }
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.HR)}, {nameof(RoleLevelEnum.Admin)}")]
    [HttpGet]
    public async Task<IActionResult> Update(Guid guid)
    {
        var employee = await _employeerepository.Get(guid);
        var employeeDtoGet = new EmployeeDtoGet();

        ViewData["isNotCollapsed"] = isNotCollapsed;
        switch (employee.Code)
        {
            case 200:
                employeeDtoGet.Guid = employee.Data!.Guid;
                employeeDtoGet.Nik = employee.Data!.Nik;
                employeeDtoGet.FirstName = employee.Data!.FirstName;
                employeeDtoGet.LastName = employee.Data.LastName;
                employeeDtoGet.BirthDate = employee.Data!.BirthDate;
                employeeDtoGet.Gender = employee.Data!.Gender;
                employeeDtoGet.HiringDate = employee.Data!.HiringDate;
                employeeDtoGet.Email = employee.Data!.Email;
                employeeDtoGet.PhoneNumber = employee.Data!.PhoneNumber;
                employeeDtoGet.Status = employee.Data!.Status;
                employeeDtoGet.GradeGuid = employee.Data.GradeGuid;
                employeeDtoGet.ProfileGuid = employee.Data.ProfileGuid;
                return View(employeeDtoGet);
            case 400:
                TempData["Error"] = employee.Message;
                return RedirectToAction(nameof(Index));
            default:
                TempData["Error"] = "Employee not found.";
                return RedirectToAction(nameof(Index));
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(EmployeeDtoGet employeeDtoGet)
    {
        var employee = await _employeerepository.Put(employeeDtoGet.Guid, employeeDtoGet);

        switch (employee.Code)
        {
            case 200:
                TempData["Success"] = employee.Message;
                return RedirectToAction(nameof(Index));
            case 400:
                TempData["Error"] = employee.Message;
                return RedirectToAction(nameof(Index));
            case 404:
                TempData["Error"] = employee.Message;
                return RedirectToAction(nameof(Index));
            default:
                TempData["Error"] = "Failed to update employee.";
                return RedirectToAction(nameof(Index));
        }
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.HR)}, {nameof(RoleLevelEnum.Admin)}")]
    [HttpPost]
    public async Task<IActionResult> Delete(Guid guid)
    {
        var employee = await _employeerepository.Delete(guid);

        switch (employee.Code)
        {
            case 200:
                TempData["Success"] = employee.Message;
                return RedirectToAction(nameof(Index));
            case 500:
                TempData["Error"] = employee.Message;
                return RedirectToAction(nameof(Index));
            case 404:
                TempData["Error"] = employee.Message;
                return RedirectToAction(nameof(Index));
            default:
                TempData["Error"] = "Failed to delete employee.";
                return RedirectToAction(nameof(Index));
        }
    }
}
