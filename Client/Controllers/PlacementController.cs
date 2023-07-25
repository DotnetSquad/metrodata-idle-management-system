using Client.Contracts;
using Client.DataTransferObjects.Placements;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers;

public class PlacementController : Controller
{
    private readonly IPlacementRepository _repository;

    public PlacementController(IPlacementRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var result = await _repository.Get();
        var listRolePlacementDtoGets = new List<PlacementDtoGet>();

        if (result.Data != null)
        {
            listRolePlacementDtoGets = result.Data.ToList();
        }
        return View(listRolePlacementDtoGets);
    }
    
}
