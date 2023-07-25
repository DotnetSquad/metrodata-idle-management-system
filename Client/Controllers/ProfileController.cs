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
}
