using Client.Contracts;
using Client.DataTransferObjects.Dashboards;
using Client.DataTransferObjects.Grades;
using Client.Utilities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers;

[Authorize]
public class FAQController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
}