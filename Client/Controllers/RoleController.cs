using Client.Contracts;
using Client.DataTransferObjects.Roles;
using Client.Utilities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers;

[Authorize(Roles = $"{nameof(RoleLevelEnum.HR)}, {nameof(RoleLevelEnum.Admin)}")]
public class RoleController : Controller
{
    private readonly IRoleRepository _roleRepository;

    public RoleController(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var result = await _roleRepository.Get();
        var listRoles = new List<RoleDtoGet>();

        if (result.Data is not null) listRoles = result.Data.ToList();

        return View(listRoles);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(RoleDtoGet roleDtoPost)
    {
        var result = await _roleRepository.Post(roleDtoPost);

        switch (result.Code)
        {
            case 200:
                TempData["Success"] = result.Message;
                return RedirectToAction(nameof(Index));
            case 400:
                TempData["Error"] = result.Message;
                return RedirectToAction(nameof(Index));
            default:
                TempData["Error"] = result.Message;
                return RedirectToAction(nameof(Index));
        }
    }

    [HttpGet]
    public async Task<IActionResult> Update(Guid guid)
    {
        var result = await _roleRepository.Get(guid);
        var role = new RoleDtoGet();

        switch (result.Code)
        {
            case 200:
                role.Guid = result.Data!.Guid;
                role.Name = result.Data!.Name;
                return View(role);
            case 400:
                TempData["Error"] = result.Message;
                return RedirectToAction(nameof(Index));
            default:
                TempData["Error"] = result.Message;
                return RedirectToAction(nameof(Index));
        }
    }

    [HttpPost]
    public async Task<IActionResult> Update(Guid id, RoleDtoGet roleDtoGet)
    {
        var result = await _roleRepository.Put(roleDtoGet.Guid, roleDtoGet);

        switch (result.Code)
        {
            case 200:
                TempData["Success"] = result.Message;
                return RedirectToAction(nameof(Index));
            case 400:
                TempData["Error"] = result.Message;
                return RedirectToAction(nameof(Index));
            case 404:
                TempData["Error"] = result.Message;
                return RedirectToAction(nameof(Index));
            default:
                TempData["Error"] = result.Message;
                return RedirectToAction(nameof(Index));
        }
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Guid guid)
    {
        var result = await _roleRepository.Delete(guid);

        switch (result.Code)
        {
            case 200:
                TempData["Success"] = result.Message;
                return RedirectToAction(nameof(Index));
            case 500:
                TempData["Error"] = result.Message;
                return RedirectToAction(nameof(Index));
            case 404:
                TempData["Error"] = result.Message;
                return RedirectToAction(nameof(Index));
            default:
                TempData["Error"] = result.Message;
                return RedirectToAction(nameof(Index));
        }
    }
}
