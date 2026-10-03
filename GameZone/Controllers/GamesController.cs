using GameZone.Services;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameZone.Controllers;

public class GamesController : Controller {
    private readonly ICategoriesService _categoriesService;
    private readonly IDevicesServices _devicesServices;

    public GamesController(ICategoriesService categoriesService, IDevicesServices devicesServices) {
        _categoriesService = categoriesService;
        _devicesServices = devicesServices;
    }

    public IActionResult Index() {
        return View();
    }

    [HttpGet]
    public IActionResult Create() {
        CreateGameFormViewModel viewModel = new() {
            Categories = _categoriesService.GetSelectedList(),
            Devices = _devicesServices.GetSelectedList()
        };
        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(CreateGameFormViewModel model) {
        if (!ModelState.IsValid) {
            model.Categories = _categoriesService.GetSelectedList();
            model.Devices = _devicesServices.GetSelectedList();

            return View(model);
        }

        // save to database
        // save cover to server 

        return RedirectToAction(nameof(Index));
    }
}
