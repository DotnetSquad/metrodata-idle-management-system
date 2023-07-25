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
}