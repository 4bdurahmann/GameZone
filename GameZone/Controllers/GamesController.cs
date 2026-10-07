using GameZone.Services;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameZone.Controllers;

public class GamesController : Controller {
    private readonly ICategoriesService _categoriesService;
    private readonly IDevicesService _devicesServices;
    private readonly IGameService _gameServices;

    public GamesController(ICategoriesService categoriesService, IDevicesService devicesServices, IGameService gameServices) {
        _categoriesService = categoriesService;
        _devicesServices = devicesServices;
        _gameServices = gameServices;
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
    public async Task<IActionResult> Create(CreateGameFormViewModel model) {
        if (!ModelState.IsValid) {
            model.Categories = _categoriesService.GetSelectedList();
            model.Devices = _devicesServices.GetSelectedList();

            return View(model);
        }

        await _gameServices.Create(model);

        return RedirectToAction(nameof(Index));
    }
}
