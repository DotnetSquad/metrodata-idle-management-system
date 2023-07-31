using Client.Contracts;
using Client.DataTransferObjects.Interviews;
using Client.DataTransferObjects.Jobs;
using Client.Utilities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers;

public class InterviewController : Controller
{
    private readonly IInterviewRepository _interviewRepository;
    private readonly IJobRepository _jobRepository;

    public InterviewController(IInterviewRepository interviewRepository, IJobRepository jobRepository)
    {
        _interviewRepository = interviewRepository;
        _jobRepository = jobRepository;
    }

    [Authorize(Roles =
        $"{nameof(RoleLevelEnum.HR)}, {nameof(RoleLevelEnum.Manager)}, {nameof(RoleLevelEnum.Trainer)}, {nameof(RoleLevelEnum.Admin)}")]
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var interviews = await _interviewRepository.Get();
        var listInterviewDtoGets = new List<InterviewDtoGet>();

        if (interviews.Data is not null) listInterviewDtoGets = interviews.Data.ToList();

        var jobs = await _jobRepository.Get();
        var listJobDtoGets = new List<JobDtoGet>();

        if (jobs.Data != null) listJobDtoGets = jobs.Data.ToList();

        ViewData["Jobs"] = listJobDtoGets;
        return View(listInterviewDtoGets);
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.HR)}, {nameof(RoleLevelEnum.Admin)}")]
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var jobs = await _jobRepository.Get();
        var listJobDtoGets = new List<JobDtoGet>();

        if (jobs.Data is not null) listJobDtoGets = jobs.Data.ToList();

        ViewData["Jobs"] = listJobDtoGets;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(InterviewDtoGet interviewDtoGet)
    {
        var interview = await _interviewRepository.Post(interviewDtoGet);

        switch (interview.Code)
        {
            case 201:
                TempData["Success"] = interview.Message;
                return RedirectToAction(nameof(Index));
            case 400:
                TempData["Error"] = interview.Message;
                return RedirectToAction(nameof(Index));
            default:
                TempData["Error"] = "Failed to create interview.";
                return RedirectToAction(nameof(Index));
        }
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.HR)}, {nameof(RoleLevelEnum.Admin)}")]
    [HttpGet]
    public async Task<IActionResult> Update(Guid guid)
    {
        var interview = await _interviewRepository.Get(guid);
        var interviewDtoGet = new InterviewDtoGet();
        if (interview.Data is null)
        {
            TempData["Error"] = interview.Message;
            return RedirectToAction(nameof(Index));
        }
        else
        {
            interviewDtoGet.Guid = interview.Data.Guid;
            interviewDtoGet.Title = interview.Data.Title;
            interviewDtoGet.Link = interview.Data.Link;
            interviewDtoGet.InterviewDate = interview.Data.InterviewDate;
            interviewDtoGet.Description = interview.Data.Description;
            interviewDtoGet.StatusInterview = interview.Data.StatusInterview;
            interviewDtoGet.JobGuid = interview.Data.JobGuid;
        }

        var jobs = await _jobRepository.Get();
        var listJobDtoGets = new List<JobDtoGet>();

        if (jobs.Data is not null) listJobDtoGets = jobs.Data.ToList();

        ViewData["Jobs"] = listJobDtoGets;
        return View(interviewDtoGet);
    }

    [HttpPost]
    public async Task<IActionResult> Update(InterviewDtoGet interviewDtoGet)
    {
        var interview = await _interviewRepository.Put(interviewDtoGet.Guid, interviewDtoGet);

        switch (interview.Code)
        {
            case 200:
                TempData["Success"] = interview.Message;
                return RedirectToAction(nameof(Index));
            case 400:
                TempData["Error"] = interview.Message;
                return RedirectToAction(nameof(Index));
            case 404:
                TempData["Error"] = interview.Message;
                return RedirectToAction(nameof(Index));
            default:
                TempData["Error"] = "Failed to update interview.";
                return RedirectToAction(nameof(Index));
        }
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.HR)}, {nameof(RoleLevelEnum.Admin)}")]
    [HttpPost]
    public async Task<IActionResult> Delete(Guid guid)
    {
        var interview = await _interviewRepository.Delete(guid);

        switch (interview.Code)
        {
            case 200:
                TempData["Success"] = interview.Message;
                return RedirectToAction(nameof(Index));
            case 404:
                TempData["Error"] = interview.Message;
                return RedirectToAction(nameof(Index));
            case 500:
                TempData["Error"] = interview.Message;
                return RedirectToAction(nameof(Index));
            default:
                TempData["Error"] = "Failed to delete interview.";
                return RedirectToAction(nameof(Index));
        }
    }
}
