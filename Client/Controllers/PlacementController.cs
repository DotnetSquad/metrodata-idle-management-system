using Client.Contracts;
using Client.DataTransferObjects.Companies;
using Client.DataTransferObjects.Employees;
using Client.DataTransferObjects.Placements;
using Client.Utilities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers;

public class PlacementController : Controller
{
    public string isNotCollapsed = "PlacementController";
    private readonly ICompanyRepository _companyRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IPlacementRepository _placementRepository;

    public PlacementController(IEmployeeRepository employeeRepository, ICompanyRepository companyRepository,
        IPlacementRepository placementRepository)
    {
        _companyRepository = companyRepository;
        _employeeRepository = employeeRepository;
        _placementRepository = placementRepository;
    }

    [Authorize(Roles =
        $"{nameof(RoleLevelEnum.HR)}, {nameof(RoleLevelEnum.Manager)}, {nameof(RoleLevelEnum.Trainer)}, {nameof(RoleLevelEnum.Admin)}, {nameof(RoleLevelEnum.Employee)}")]
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var placements = await _placementRepository.Get();
        var listRolePlacementDtoGets = new List<PlacementDtoGet>();

        if (placements.Data is not null)
        {
            listRolePlacementDtoGets = placements.Data.ToList();
        }

        var employees = await _employeeRepository.Get();
        var listEmployeeDtoGets = new List<EmployeeDtoGet>();

        if (employees.Data is not null)
        {
            listEmployeeDtoGets = employees.Data.ToList();
        }

        var companies = await _companyRepository.Get();
        var listCompanyDtoGets = new List<CompanyDtoGet>();

        if (companies.Data is not null)
        {
            listCompanyDtoGets = companies.Data.ToList();
        }

        ViewData["isNotCollapsed"] = isNotCollapsed;
        ViewData["Employees"] = listEmployeeDtoGets;
        ViewData["Companies"] = listCompanyDtoGets;

        return View(listRolePlacementDtoGets);
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.HR)}, {nameof(RoleLevelEnum.Admin)}")]
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var employees = await _employeeRepository.Get();
        var listEmployeeDtoGets = new List<EmployeeDtoGet>();

        if (employees.Data is not null)
        {
            listEmployeeDtoGets = employees.Data.ToList();
        }

        var companies = await _companyRepository.Get();
        var listCompanyDtoGets = new List<CompanyDtoGet>();

        if (companies.Data is not null)
        {
            listCompanyDtoGets = companies.Data.ToList();
        }

        ViewData["isNotCollapsed"] = isNotCollapsed;
        ViewData["Employees"] = listEmployeeDtoGets;
        ViewData["Companies"] = listCompanyDtoGets;

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(PlacementDtoGet placementDtoGet)
    {
        var placement = await _placementRepository.Post(placementDtoGet);

        switch (placement.Code)
        {
            case 201:
                TempData["Success"] = placement.Message;
                return RedirectToAction(nameof(Index));
            case 400:
                TempData["Error"] = placement.Message;
                return RedirectToAction(nameof(Index));
            default:
                TempData["Error"] = "Failed to create placement.";
                return RedirectToAction(nameof(Index));
        }
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.HR)}, {nameof(RoleLevelEnum.Admin)}")]
    [HttpGet]
    public async Task<IActionResult> Update(Guid guid)
    {
        var placement = await _placementRepository.Get(guid);
        var placementDtoGet = new PlacementDtoGet();

        if (placement.Data is null)
        {
            TempData["Error"] = placement.Message;
            return RedirectToAction(nameof(Index));
        }
        else
        {
            placementDtoGet.Guid = placement.Data!.Guid;
            placementDtoGet.Title = placement.Data!.Title;
            placementDtoGet.Description = placement.Data!.Description;
            placementDtoGet.EmployeeGuid = placement.Data!.EmployeeGuid;
            placementDtoGet.CompanyGuid = placement.Data!.CompanyGuid;
        }

        var employees = await _employeeRepository.Get();
        var listEmployeeDtoGets = new List<EmployeeDtoGet>();

        if (employees.Data is not null)
        {
            listEmployeeDtoGets = employees.Data.ToList();
        }

        var companies = await _companyRepository.Get();
        var listCompanyDtoGets = new List<CompanyDtoGet>();

        if (companies.Data is not null)
        {
            listCompanyDtoGets = companies.Data.ToList();
        }

        ViewData["isNotCollapsed"] = isNotCollapsed;
        ViewData["Employees"] = listEmployeeDtoGets;
        ViewData["Companies"] = listCompanyDtoGets;

        return View(placementDtoGet);
    }

    [HttpPost]
    public async Task<IActionResult> Update(PlacementDtoGet placementDtoGet)
    {
        var result = await _placementRepository.Put(placementDtoGet.Guid, placementDtoGet);

        switch (result.Code)
        {
            case 200:
                TempData["Success"] = result.Message;
                return RedirectToAction(nameof(Index));
            case 400:
                TempData["Error"] = result.Message;
                return RedirectToAction(nameof(Index));
            case 404:
                TempData["Error"] = result.Message;
                return RedirectToAction(nameof(Index));
            default:
                TempData["Error"] = "Failed to update placement.";
                return RedirectToAction(nameof(Index));
        }
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.HR)}, {nameof(RoleLevelEnum.Admin)}")]
    [HttpPost]
    public async Task<IActionResult> Delete(Guid guid)
    {
        var placement = await _placementRepository.Delete(guid);

        switch (placement.Code)
        {
            case 200:
                TempData["Success"] = placement.Message;
                return RedirectToAction(nameof(Index));
            case 404:
                TempData["Error"] = placement.Message;
                return RedirectToAction(nameof(Index));
            case 500:
                TempData["Error"] = placement.Message;
                return RedirectToAction(nameof(Index));
            default:
                TempData["Error"] = "Failed to delete placement.";
                return RedirectToAction(nameof(Index));
        }
    }
}
