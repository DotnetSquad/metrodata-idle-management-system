using Client.Contracts;
using Client.DataTransferObjects.Companies;
using Client.DataTransferObjects.Jobs;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers;

public class JobController : Controller
{
    private readonly IJobRepository _repository;
    private readonly ICompanyRepository _companyRepository;
    public JobController(IJobRepository repository, ICompanyRepository companyRepository)
    {
        _repository = repository;
        _companyRepository = companyRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var result = await _repository.Get();
        var ListJob = new List<JobDtoGet>();

        if (result.Data != null)
        {
            ListJob = result.Data.ToList();
        }
        return View(ListJob);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        // get companies
        var resultCompany = await _companyRepository.Get();
        var listCompanyDtoGets = new List<CompanyDtoGet>();

        if (resultCompany.Data != null)
        {
            listCompanyDtoGets = resultCompany.Data.ToList();
        }

        // add to view data
        ViewData["Companies"] = listCompanyDtoGets;

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(JobDtoGet jobDtoPost)
    {
        var result = await _repository.Post(jobDtoPost);
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
        // get companies
        var resultCompany = await _companyRepository.Get();
        var listCompanyDtoGets = new List<CompanyDtoGet>();

        if (resultCompany.Data != null)
        {
            listCompanyDtoGets = resultCompany.Data.ToList();
        }

        // add to view data
        ViewData["Companies"] = listCompanyDtoGets;

        var value = await _repository.Get(guid);
        var job = new JobDtoGet();
        if (value.Data?.Guid is null)
        {
            return View(job);
        }
        else
        {
            job.Guid = value.Data.Guid;
            job.JobName = value.Data.JobName;
            job.Description = value.Data.Description;
            job.CompanyGuid = value.Data.CompanyGuid;
        }

        return View(job);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(JobDtoGet job)
    {
        if (ModelState.IsValid)
        {
            var result = await _repository.Put(job.Guid, job);
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
