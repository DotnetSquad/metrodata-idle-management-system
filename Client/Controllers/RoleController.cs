using Client.Contracts;
using Client.DataTransferObjects.Roles;
using Client.Utilities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers;

[Authorize(Roles = $"{nameof(RoleLevelEnum.HR)}, {nameof(RoleLevelEnum.Admin)}")]
public class RoleController : Controller
{
    public string isNotCollapsed = "RoleController";
    private readonly IRoleRepository _roleRepository;

    public RoleController(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var roles = await _roleRepository.Get();
        var listRoles = new List<RoleDtoGet>();

        if (roles.Data is not null) listRoles = roles.Data.ToList();

        ViewData["isNotCollapsed"] = isNotCollapsed;
        return View(listRoles);
    }

    [HttpGet]
    public IActionResult Create()
    {
        ViewData["isNotCollapsed"] = isNotCollapsed;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(RoleDtoGet roleDtoPost)
    {
        var role = await _roleRepository.Post(roleDtoPost);

        switch (role.Code)
        {
            case 201:
                TempData["Success"] = role.Message;
                return RedirectToAction(nameof(Index));
            case 400:
                TempData["Error"] = role.Message;
                return RedirectToAction(nameof(Index));
            default:
                TempData["Error"] = "Failed to create role.";
                return RedirectToAction(nameof(Index));
        }
    }

    [HttpGet]
    public async Task<IActionResult> Update(Guid guid)
    {
        var role = await _roleRepository.Get(guid);
        var roleDtoGet = new RoleDtoGet();

        ViewData["isNotCollapsed"] = isNotCollapsed;
        switch (role.Code)
        {
            case 200:
                roleDtoGet.Guid = role.Data!.Guid;
                roleDtoGet.Name = role.Data!.Name;
                return View(roleDtoGet);
            case 400:
                TempData["Error"] = role.Message;
                return RedirectToAction(nameof(Index));
            default:
                TempData["Error"] = "Role not found.";
                return RedirectToAction(nameof(Index));
        }
    }

    [HttpPost]
    public async Task<IActionResult> Update(Guid id, RoleDtoGet roleDtoGet)
    {
        var role = await _roleRepository.Put(roleDtoGet.Guid, roleDtoGet);

        switch (role.Code)
        {
            case 200:
                TempData["Success"] = role.Message;
                return RedirectToAction(nameof(Index));
            case 400:
                TempData["Error"] = role.Message;
                return RedirectToAction(nameof(Index));
            case 404:
                TempData["Error"] = role.Message;
                return RedirectToAction(nameof(Index));
            default:
                TempData["Error"] = "Failed to update role.";
                return RedirectToAction(nameof(Index));
        }
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Guid guid)
    {
        var role = await _roleRepository.Delete(guid);

        switch (role.Code)
        {
            case 200:
                TempData["Success"] = role.Message;
                return RedirectToAction(nameof(Index));
            case 500:
                TempData["Error"] = role.Message;
                return RedirectToAction(nameof(Index));
            case 404:
                TempData["Error"] = role.Message;
                return RedirectToAction(nameof(Index));
            default:
                TempData["Error"] = "Failed to delete role.";
                return RedirectToAction(nameof(Index));
        }
    }
}
