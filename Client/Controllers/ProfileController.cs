﻿using Client.Contracts;
using Client.DataTransferObjects.Employees;
using Client.DataTransferObjects.Profiles;
using Client.Utilities.Enums;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers;

public class ProfileController : Controller
{
    public string isNotCollapsed = "ProfileController";
    private readonly IProfileRepository _profileRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly Cloudinary _cloudinary;

    public ProfileController(IProfileRepository profileRepository, IEmployeeRepository employeeRepository, Cloudinary cloudinary)
    {
        _profileRepository = profileRepository;
        _employeeRepository = employeeRepository;
        _cloudinary = cloudinary;
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

        ViewData["isNotCollapsed"] = isNotCollapsed;
        return View(ListProfiles);
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.Employee)}, {nameof(RoleLevelEnum.Admin)}")]
    [HttpGet]
    public IActionResult Create()
    {
        ViewData["isNotCollapsed"] = isNotCollapsed;
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
                TempData["Error"] = "Failed to create profile.";
                return RedirectToAction(nameof(Index));
        }
    }

    [Authorize(Roles = $"{nameof(RoleLevelEnum.Employee)}, {nameof(RoleLevelEnum.Admin)}")]
    [HttpGet]
    public async Task<IActionResult> Update(Guid guid)
    {
        var profile = await _profileRepository.Get(guid);
        var profileDtoGet = new ProfileDtoGet();

        ViewData["isNotCollapsed"] = isNotCollapsed;
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
                TempData["Error"] = "Profile not found.";
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
                TempData["Error"] = "Failed to update profile.";
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
                TempData["Error"] = "Failed to delete profile";
                return RedirectToAction(nameof(Index));
        }
    }

    [HttpGet]
    public async Task<IActionResult> Details(Guid guid)
    {
        var employeeGuid = User.Claims.FirstOrDefault(x => x.Type == "Guid")?.Value;
        var employeeGuidTemp = Guid.Parse(employeeGuid);

        var profile = await _profileRepository.Get(guid);
        var profileDtoGet = new ProfileDtoGet();

        if (profile.Data is not null) profileDtoGet = profile.Data;

        var employee = await _employeeRepository.Get(employeeGuidTemp);
        var employeeDtoGet = new EmployeeDtoGet();

        if (employee.Data is not null) employeeDtoGet = employee.Data;


        ViewData["isNotCollapsed"] = isNotCollapsed;

        var employeeProfileDtoGet = new EmployeeProfileDtoGet
        {
            EmployeeGuid = employeeDtoGet.Guid,
            Nik = employeeDtoGet.Nik,
            FirstName = employeeDtoGet.FirstName,
            LastName = employeeDtoGet.LastName,
            BirthDate = employeeDtoGet.BirthDate,
            Gender = employeeDtoGet.Gender,
            HiringDate = employeeDtoGet.HiringDate,
            Email = employeeDtoGet.Email,
            PhoneNumber = employeeDtoGet.PhoneNumber,
            Status = employeeDtoGet.Status,
            GradeGuid = employeeDtoGet.GradeGuid,
            ProfileGuidInEmployee = employeeDtoGet.ProfileGuid,
            ProfileGuid = profileDtoGet.Guid,
            PhotoProfile = profileDtoGet.PhotoProfile,
            Skills = profileDtoGet.Skills,
            Linkedin = profileDtoGet.Linkedin,
            Resume = profileDtoGet.Resume
        };

        switch (profile.Code)
        {
            case 200:
                return View(employeeProfileDtoGet);
            case 400:
                TempData["Error"] = profile.Message;
                return RedirectToAction(nameof(Index));
            default:
                TempData["Error"] = "Data profile not found.";
                return RedirectToAction(nameof(Index));
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateEmployeeProfile(EmployeeProfileDtoGet employeeProfileDtoGet)
    {
        var profile = await _profileRepository.Get(employeeProfileDtoGet.ProfileGuid);

        var profileDtoGet = new ProfileDtoGet
        {
            Guid = employeeProfileDtoGet.ProfileGuid,
            Skills = employeeProfileDtoGet.Skills,
            Linkedin = employeeProfileDtoGet.Linkedin,
            Resume = employeeProfileDtoGet.Resume
        };

        var employeeDtoGet = new EmployeeDtoGet
        {
            Guid = employeeProfileDtoGet.EmployeeGuid,
            Nik = employeeProfileDtoGet.Nik,
            FirstName = employeeProfileDtoGet.FirstName,
            LastName = employeeProfileDtoGet.LastName,
            BirthDate = employeeProfileDtoGet.BirthDate,
            Gender = employeeProfileDtoGet.Gender,
            HiringDate = employeeProfileDtoGet.HiringDate,
            Email = employeeProfileDtoGet.Email,
            PhoneNumber = employeeProfileDtoGet.PhoneNumber,
            Status = employeeProfileDtoGet.Status,
            GradeGuid = employeeProfileDtoGet.GradeGuid,
            ProfileGuid = employeeProfileDtoGet.ProfileGuidInEmployee
        };

        // get photo profile file and upload to cloudinary
        // if photo profile file is null, then use default photo profile
        string photoProfile = "";

        if (HttpContext.Request.Form.Files.Count != 0)
        {
            var file = HttpContext.Request.Form.Files[0];
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, file.OpenReadStream())
            };
            var uploadResult = _cloudinary.Upload(uploadParams);

            photoProfile = uploadResult.Url.ToString();
        }
        else
        {
            photoProfile = profile.Data!.PhotoProfile;
        }       

        profileDtoGet.PhotoProfile = photoProfile;

        var profileUpdated = await _profileRepository.Put(profileDtoGet.Guid, profileDtoGet);
        var employeeUpdated = await _employeeRepository.Put(employeeDtoGet.Guid, employeeDtoGet);

        switch (profileUpdated.Code)
        {
            case 200:
                TempData["Success"] = profile.Message;
                return RedirectToAction("Details", "Profile", new { guid = profileDtoGet.Guid });
            case 400:
                TempData["Error"] = profile.Message;
                return RedirectToAction("Details", "Profile", new { guid = profileDtoGet.Guid });
            case 404:
                TempData["Error"] = profile.Message;
                return RedirectToAction("Details", "Profile", new { guid = profileDtoGet.Guid });
            default:
                TempData["Error"] = "Failed to update profile.";
                return RedirectToAction("Details", "Profile", new { guid = profileDtoGet.Guid });
        }
    }
}
