using Client.Contracts;
using Client.DataTransferObjects.Roles;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers;

public class RoleController : Controller
{
    private readonly IRoleRepository _repository;

    public RoleController(IRoleRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var result = await _repository.Get();
        var ListRole = new List<RoleDtoGet>();

        if (result.Data != null)
        {
            ListRole = result.Data.ToList();
        }

        return View(ListRole);
    }

    // create 
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(RoleDtoGet roleDtoPost)
    {
        var result = await _repository.Post(roleDtoPost);
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
