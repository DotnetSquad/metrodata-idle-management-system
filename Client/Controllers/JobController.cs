using Client.Contracts;
using Client.DataTransferObjects.Companies;
using Client.DataTransferObjects.Jobs;
using Client.Utilities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers;

public class JobController : Controller
{
    private readonly IJobRepository _jobRepository;
    private readonly ICompanyRepository _companyRepository;

    public JobController(IJobRepository jobRepository, ICompanyRepository companyRepository)
    {
        _jobRepository = jobRepository;
        _companyRepository = companyRepository;
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.Employee)}, {nameof(RoleLevelEnum.Admin)}")]
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var jobs = await _jobRepository.Get();
        var listJobDtoGets = new List<JobDtoGet>();

        if (jobs.Data is not null)
        {
            listJobDtoGets = jobs.Data.ToList();
        }

        // get companies
        var companies = await _companyRepository.Get();
        var listCompanyDtoGets = new List<CompanyDtoGet>();

        if (companies.Data is not null)
        {
            listCompanyDtoGets = companies.Data.ToList();
        }

        // add to view data
        ViewData["Companies"] = listCompanyDtoGets;

        return View(listJobDtoGets);
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.HR)}, {nameof(RoleLevelEnum.Admin)}")]
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        // get companies
        var companies = await _companyRepository.Get();
        var listCompanyDtoGets = new List<CompanyDtoGet>();

        if (companies.Data is not null)
        {
            listCompanyDtoGets = companies.Data.ToList();
        }

        // add to view data
        ViewData["Companies"] = listCompanyDtoGets;

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(JobDtoGet jobDtoPost)
    {
        var job = await _jobRepository.Post(jobDtoPost);

        switch (job.Code)
        {
            case 201:
                TempData["Success"] = job.Message;
                return RedirectToAction(nameof(Index));
            case 400:
                TempData["Error"] = job.Message;
                return RedirectToAction(nameof(Index));
            default:
                TempData["Error"] = "Failed to create job.";
                return RedirectToAction(nameof(Index));
        }
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.HR)}, {nameof(RoleLevelEnum.Admin)}")]
    [HttpGet]
    public async Task<IActionResult> Update(Guid guid)
    {
        // get companies
        var companies = await _companyRepository.Get();
        var listCompanyDtoGets = new List<CompanyDtoGet>();

        if (companies.Data is not null)
        {
            listCompanyDtoGets = companies.Data.ToList();
        }

        // add to view data
        ViewData["Companies"] = listCompanyDtoGets;

        var job = await _jobRepository.Get(guid);
        var jobDtoGet = new JobDtoGet();

        if (job.Data is null)
        {
            TempData["Error"] = job.Message;
            return RedirectToAction(nameof(Index));
        }
        else
        {
            jobDtoGet.Guid = job.Data.Guid;
            jobDtoGet.JobName = job.Data.JobName;
            jobDtoGet.Description = job.Data.Description;
            jobDtoGet.CompanyGuid = job.Data.CompanyGuid;
        }

        return View(jobDtoGet);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(JobDtoGet jobDtoGet)
    {
        var job = await _jobRepository.Put(jobDtoGet.Guid, jobDtoGet);

        switch (job.Code)
        {
            case 200:
                TempData["Success"] = job.Message;
                return RedirectToAction(nameof(Index));
            case 400:
                TempData["Error"] = job.Message;
                return RedirectToAction(nameof(Index));
            case 404:
                TempData["Error"] = job.Message;
                return RedirectToAction(nameof(Index));
            default:
                TempData["Error"] = "Failed to update job.";
                return RedirectToAction(nameof(Index));
        }
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.HR)}, {nameof(RoleLevelEnum.Admin)}")]
    [HttpPost]
    public async Task<IActionResult> Delete(Guid guid)
    {
        var job = await _jobRepository.Delete(guid);

        switch (job.Code)
        {
            case 200:
                TempData["Success"] = job.Message;
                return RedirectToAction(nameof(Index));
            case 404:
                TempData["Error"] = job.Message;
                return RedirectToAction(nameof(Index));
            case 500:
                TempData["Error"] = job.Message;
                return RedirectToAction(nameof(Index));
            default:
                TempData["Error"] = "Failed to delete job.";
                return RedirectToAction(nameof(Index));
        }
    }
}
