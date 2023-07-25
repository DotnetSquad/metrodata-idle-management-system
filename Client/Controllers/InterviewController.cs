using Client.Contracts;
using Client.DataTransferObjects.Interviews;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers;

public class InterviewController : Controller
{
    private readonly IInterviewRepository _repository;

    public InterviewController(IInterviewRepository repository)
    {
        _repository = repository;
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
        return View(listInterviewDtoGets);
    }
}
