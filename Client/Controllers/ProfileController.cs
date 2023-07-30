using Client.Contracts;
using Client.DataTransferObjects.Profiles;
using Client.Utilities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers;

public class ProfileController : Controller
{
    private readonly IProfileRepository _profileRepository;

    public ProfileController(IProfileRepository profileRepository)
    {
        _profileRepository = profileRepository;
    }

    [Authorize(Roles =
        $"{nameof(RoleLevelEnum.HR)}, {nameof(RoleLevelEnum.Manager)}, {nameof(RoleLevelEnum.Trainer)}, {nameof(RoleLevelEnum.Admin)}")]
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var profiles = await _profileRepository.Get();
        var ListProfiles = new List<ProfileDtoGet>();

        if (profiles.Data is not null)
        {
            ListProfiles = profiles.Data.ToList();
        }

        return View(ListProfiles);
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.Employee)}, {nameof(RoleLevelEnum.Admin)}")]
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProfileDtoGet profileDtoPost)
    {
        var profile = await _profileRepository.Post(profileDtoPost);
        switch (profile.Code)
        {
            case 201:
                TempData["Success"] = profile.Message;
                return RedirectToAction(nameof(Index));
            case 400:
                TempData["Error"] = profile.Message;
                return RedirectToAction(nameof(Index));
            default:
                TempData["Error"] = profile.Message;
                return RedirectToAction(nameof(Index));
        }
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.Employee)}, {nameof(RoleLevelEnum.Admin)}")]
    [HttpGet]
    public async Task<IActionResult> Update(Guid guid)
    {
        var profile = await _profileRepository.Get(guid);
        var profileDtoGet = new ProfileDtoGet();
        switch (profile.Code)
        {
            case 200:
                profileDtoGet.Guid = profile.Data!.Guid;
                profileDtoGet.Skills = profile.Data!.Skills;
                profileDtoGet.Linkedin = profile.Data!.Linkedin;
                profileDtoGet.Resume = profile.Data!.Resume;
                return View(profileDtoGet);
            case 400:
                TempData["Error"] = profile.Message;
                return RedirectToAction(nameof(Index));
            default:
                TempData["Error"] = profile.Message;
                return RedirectToAction(nameof(Index));
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(Guid id, ProfileDtoGet profileDtoGet)
    {

        var profile = await _profileRepository.Put(profileDtoGet.Guid, profileDtoGet);
        switch (profile.Code)
        {
            case 200:
                TempData["Success"] = profile.Message;
                return RedirectToAction(nameof(Index));
            case 400:
                TempData["Error"] = profile.Message;
                return RedirectToAction(nameof(Index));
            case 404:
                TempData["Error"] = profile.Message;
                return RedirectToAction(nameof(Index));
            default:
                TempData["Error"] = profile.Message;
                return RedirectToAction(nameof(Index));
        }
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.HR)}, {nameof(RoleLevelEnum.Admin)}")]
    [HttpPost]
    public async Task<IActionResult> Delete(Guid guid)
    {
        var profile = await _profileRepository.Delete(guid);
        switch (profile.Code)
        {
            case 200:
                TempData["Success"] = profile.Message;
                return RedirectToAction(nameof(Index));
            case 500:
                TempData["Error"] = profile.Message;
                return RedirectToAction(nameof(Index));
            case 404:
                TempData["Error"] = profile.Message;
                return RedirectToAction(nameof(Index));
            default:
                TempData["Error"] = profile.Message;
                return RedirectToAction(nameof(Index));
        }
    }
}