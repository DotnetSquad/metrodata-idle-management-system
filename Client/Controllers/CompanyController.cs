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

    [HttpGet]
    public async Task<IActionResult> Update(Guid guid)
    {
        var result = await _companyRepository.Get(guid);
        var company = new CompanyDtoGet();
        if (result.Data?.Guid is null)
        {
            return View(company);
        }
        else
        {
            company.Guid = result.Data.Guid;
            company.CompanyName = result.Data.CompanyName;
            company.Description = result.Data.Description;
            company.Address = result.Data.Address;
        }

        return View(company);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(Guid id, CompanyDtoGet companyDtoGet)
    {
        if (ModelState.IsValid)
        {
            var result = await _companyRepository.Put(companyDtoGet.Guid, companyDtoGet);
            if (result.Code == 200)
            {
                return RedirectToAction(nameof(Index));
            }
            else if (result.Code == 409)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View();
            }
        }
        return View();
    }
}