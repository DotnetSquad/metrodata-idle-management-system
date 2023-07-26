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

    [HttpGet]
    public async Task<IActionResult> Update(Guid guid)
    {
        var result = await _repository.Get(guid);
        var profile = new ProfileDtoGet();
        if (result.Data?.Guid is null)
        {
            return View(profile);
        }
        else
        {
            profile.Guid = result.Data.Guid;
            profile.Skills = result.Data.Skills;
            profile.Linkedin = result.Data.Linkedin;
            profile.Resume = result.Data.Resume;
        }

        return View(profile);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(Guid id, ProfileDtoGet profileDtoGet)
    {
        if (ModelState.IsValid)
        {
            var result = await _repository.Put(profileDtoGet.Guid, profileDtoGet);
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
