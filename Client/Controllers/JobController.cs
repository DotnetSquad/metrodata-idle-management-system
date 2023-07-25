using Client.Contracts;
using Client.DataTransferObjects.Jobs;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers;

public class JobController : Controller
{
    private readonly IJobRepository _repository;

    public JobController(IJobRepository repository)
    {
        _repository = repository;
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
}
