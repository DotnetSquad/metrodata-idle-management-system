using Client.Contracts;
using Client.DataTransferObjects.Companies;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers;

public class CompanyController : Controller
{
    private readonly ICompanyRepository _companyRepository;

    public CompanyController(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var result = await _companyRepository.Get();
        var ListCompany = new List<CompanyDtoGet>();

        if (result.Data != null)
        {
            ListCompany = result.Data.ToList();
        }
        return View(ListCompany);
    }

    // create 
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CompanyDtoGet companyDtoPost)
    {
        var result = await _companyRepository.Post(companyDtoPost);
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
}