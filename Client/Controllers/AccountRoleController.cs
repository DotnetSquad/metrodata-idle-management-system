using Client.Contracts;
using Client.DataTransferObjects.AccountRoles;
using Client.DataTransferObjects.Employees;
using Client.DataTransferObjects.Roles;
using Client.Utilities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers;

public class AccountRoleController : Controller
{
    private readonly IAccountRoleRepository _accountRoleRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IRoleRepository _roleRepository;

    public AccountRoleController(IAccountRoleRepository accountRoleRepository, IEmployeeRepository employeeRepository,
        IRoleRepository roleRepository)
    {
        _accountRoleRepository = accountRoleRepository;
        _employeeRepository = employeeRepository;
        _roleRepository = roleRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Index(Guid guid)
    {
        var accountRoles = await _accountRoleRepository.Get();
        var listAccountRoles = new List<AccountRoleDtoGet>();
          
        if (accountRoles.Data is not null)
        {
            // Filter account roles based on the given guid
            listAccountRoles = accountRoles.Data.Where(ar => ar.RoleGuid == guid).ToList();
        }
        
        var employees = await _employeeRepository.GetByRole(guid);
        var listEmployees = new List<EmployeeDtoGet>();

        if (employees.Data is not null)
        {
            listEmployees = employees.Data.ToList();
        }

        ViewData["RoleGuid"] = guid;
        ViewData["AccountRoles"] = listAccountRoles;

        return View(listEmployees);
    }

    [HttpGet]
    public async Task<IActionResult> Create(Guid guid)
    {
        var employeesExcludeRole = await _employeeRepository.GetExcludeRole(guid);
        var roles = await _roleRepository.Get(guid);
        var listEmployeeDtoGets = new List<EmployeeDtoGet>();

        if (employeesExcludeRole.Data is not null)
        {
            listEmployeeDtoGets = employeesExcludeRole.Data.ToList();
        }

        ViewData["RoleGuid"] = guid;
        ViewData["Employees"] = listEmployeeDtoGets;
        
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(AccountRoleDtoGet accountRoleDtoGet)
    {
        var accountRole = await _accountRoleRepository.Post(accountRoleDtoGet);
        if (accountRole is null)
        {
            return RedirectToAction("Create", new {guid = accountRoleDtoGet.RoleGuid});
        }

        return RedirectToAction("Index", new {guid = accountRoleDtoGet.RoleGuid});
    }
    
    [Authorize(Roles = $"{nameof(RoleLevelEnum.HR)}, {nameof(RoleLevelEnum.Admin)}")]
    [HttpPost]
    public async Task<IActionResult> Delete(Guid guid)
    {
        var accountRole = await _accountRoleRepository.Delete(guid);

        switch (accountRole.Code)
        {
            case 200:
                TempData["Success"] = accountRole.Message;
                return RedirectToAction("Index", "Role");
            case 404:
                TempData["Error"] = accountRole.Message;
                return RedirectToAction("Index", "Role");
            case 500:
                TempData["Error"] = accountRole.Message;
                return RedirectToAction("Index", "Role");
            default:
                TempData["Error"] = "An error occurred, please try again later.";
                return RedirectToAction("Index", "Role");
        }
    }
}
