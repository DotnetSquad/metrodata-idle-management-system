using Client.Contracts;
using Client.DataTransferObjects.Companies;
using Client.DataTransferObjects.Employees;
using Client.DataTransferObjects.Placements;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers;

public class PlacementController : Controller
{
    private readonly ICompanyRepository companyRepository;
    private readonly IEmployeeRepository employeeRepository;
    private readonly IPlacementRepository _repository;

    public PlacementController(IEmployeeRepository employeeRepository, ICompanyRepository companyRepository, IPlacementRepository repository)
    {
        this.employeeRepository = employeeRepository;
        this.companyRepository = companyRepository;
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var result = await _repository.Get();
        var listRolePlacementDtoGets = new List<PlacementDtoGet>();

        if (result.Data != null)
        {
            listRolePlacementDtoGets = result.Data.ToList();
        }
        return View(listRolePlacementDtoGets);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        // get employees
        var result = await employeeRepository.Get();
        var listEmployeeDtoGets = new List<EmployeeDtoGet>();

        if (result.Data != null)
        {
            listEmployeeDtoGets = result.Data.ToList();
        }

        // get companies
        var resultCompany = await companyRepository.Get();
        var listCompanyDtoGets = new List<CompanyDtoGet>();

        if (resultCompany.Data != null)
        {
            listCompanyDtoGets = resultCompany.Data.ToList();
        }

        // add to view data
        ViewData["Employees"] = listEmployeeDtoGets;
        ViewData["Companies"] = listCompanyDtoGets;

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(PlacementDtoGet placementDtoGet)
    {
        var result = await _repository.Post(placementDtoGet);
        if (result.Code == 201)
        {
            TempData["Success"] = "Data success created";
            return RedirectToAction(nameof(Index));
        }
        else if (result.Code == 409)
        {
            ModelState.AddModelError(string.Empty, result.Message);
            return View();
        }
        return RedirectToAction(nameof(Index));
    }
}
