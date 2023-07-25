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
}
