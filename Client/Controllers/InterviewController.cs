using Client.Contracts;
using Client.DataTransferObjects.Interviews;
using Client.DataTransferObjects.Jobs;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers;

public class InterviewController : Controller
{
    private readonly IInterviewRepository _repository;
    private readonly IJobRepository _jobRepository;

    public InterviewController(IInterviewRepository repository, IJobRepository jobRepository)
    {
        _repository = repository;
        _jobRepository = jobRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var result = await _repository.Get();
        var listInterviewDtoGets = new List<InterviewDtoGet>();

        if (result.Data != null)
        {
            listInterviewDtoGets = result.Data.ToList();
        }
        
        // get job
        var resultJob = await _jobRepository.Get();
        var listJobDtoGets = new List<JobDtoGet>();
        
        if (resultJob.Data != null)
        {
            listJobDtoGets = resultJob.Data.ToList();
        }
        
        // add to view data
        ViewData["Jobs"] = listJobDtoGets;
        
        return View(listInterviewDtoGets);
    }
    
    // create
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        // get job
        var resultJob = await _jobRepository.Get();
        var listJobDtoGets = new List<JobDtoGet>();
        
        if (resultJob.Data != null)
        {
            listJobDtoGets = resultJob.Data.ToList();
        }
        
        // add to view data
        ViewData["Jobs"] = listJobDtoGets;
        
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(InterviewDtoGet interviewDtoGet)
    {
        var result = await _repository.Post(interviewDtoGet);
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
