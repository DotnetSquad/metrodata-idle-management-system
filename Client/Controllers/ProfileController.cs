using Client.Contracts;
using Client.DataTransferObjects.Profiles;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers;

public class ProfileController : Controller
{
    private readonly IProfileRepository _repository;

    public ProfileController(IProfileRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var result = await _repository.Get();
        var ListProfile = new List<ProfileDtoGet>();

        if (result.Data != null)
        {
            ListProfile = result.Data.ToList();
        }
        return View(ListProfile);
    }

    // create 
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProfileDtoGet profileDtoPost)
    {
        var result = await _repository.Post(profileDtoPost);
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
