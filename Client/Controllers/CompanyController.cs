using Client.Contracts;
using Client.DataTransferObjects.Companies;
using Client.Utilities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers;

public class CompanyController : Controller
{
    public string isNotCollapsed = "CompanyController";
    private readonly ICompanyRepository _companyRepository;

    public CompanyController(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.Employee)}, {nameof(RoleLevelEnum.Admin)}, {nameof(RoleLevelEnum.HR)}")]
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var companies = await _companyRepository.Get();
        var ListCompanies = new List<CompanyDtoGet>();

        if (companies.Data is not null)
        {
            ListCompanies = companies.Data.ToList();
        }

        ViewData["isNotCollapsed"] = isNotCollapsed;
        return View(ListCompanies);
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.HR)}, {nameof(RoleLevelEnum.Admin)}")]
    [HttpGet]
    public IActionResult Create()
    {
        ViewData["isNotCollapsed"] = isNotCollapsed;
        return View();
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.HR)}, {nameof(RoleLevelEnum.Admin)}")]
    [ValidateAntiForgeryToken]
    [HttpPost]
    public async Task<IActionResult> Create(CompanyDtoGet companyDtoPost)
    {
        var company = await _companyRepository.Post(companyDtoPost);

        switch (company.Code)
        {
            case 201:
                TempData["Success"] = company.Message;
                return RedirectToAction(nameof(Index));
            case 400:
                TempData["Error"] = company.Message;
                return RedirectToAction(nameof(Index));
            default:
                TempData["Error"] = "Failed to create company.";
                return RedirectToAction(nameof(Index));
        }
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.HR)}, {nameof(RoleLevelEnum.Admin)}")]
    [HttpGet]
    public async Task<IActionResult> Update(Guid guid)
    {
        var company = await _companyRepository.Get(guid);
        var companyDtoGet = new CompanyDtoGet();

        ViewData["isNotCollapsed"] = isNotCollapsed;
        switch (company.Code)
        {
            case 200:
                companyDtoGet.Guid = company.Data!.Guid;
                companyDtoGet.CompanyName = company.Data!.CompanyName;
                companyDtoGet.Description = company.Data!.Description;
                companyDtoGet.Address = company.Data!.Address;
                return View(companyDtoGet);
            case 400:
                TempData["Error"] = company.Message;
                return RedirectToAction(nameof(Index));
            default:
                TempData["Error"] = "Company not found.";
                return RedirectToAction(nameof(Index));
        }
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.HR)}, {nameof(RoleLevelEnum.Admin)}")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(Guid id, CompanyDtoGet companyDtoGet)
    {
        var company = await _companyRepository.Put(companyDtoGet.Guid, companyDtoGet);

        switch (company.Code)
        {
            case 200:
                TempData["Success"] = company.Message;
                return RedirectToAction(nameof(Index));
            case 400:
                TempData["Error"] = company.Message;
                return RedirectToAction(nameof(Index));
            case 404:
                TempData["Error"] = company.Message;
                return RedirectToAction(nameof(Index));
            default:
                TempData["Error"] = "Failed to update company.";
                return RedirectToAction(nameof(Index));
        }
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.HR)}, {nameof(RoleLevelEnum.Admin)}")]
    [HttpPost]
    public async Task<IActionResult> Delete(Guid guid)
    {
        var company = await _companyRepository.Delete(guid);

        switch (company.Code)
        {
            case 200:
                TempData["Success"] = company.Message;
                return RedirectToAction(nameof(Index));
            case 500:
                TempData["Error"] = company.Message;
                return RedirectToAction(nameof(Index));
            case 404:
                TempData["Error"] = company.Message;
                return RedirectToAction(nameof(Index));
            default:
                TempData["Error"] = "Failed to delete company.";
                return RedirectToAction(nameof(Index));
        }
    }
}
