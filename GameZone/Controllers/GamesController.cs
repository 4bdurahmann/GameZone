using GameZone.Services;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameZone.Controllers;

public class GamesController : Controller {
	private readonly ICategoriesService _categoriesService;
	private readonly IDevicesService _devicesService;
	private readonly IGameService _gameService;

	public GamesController(ICategoriesService categoriesService, IDevicesService devicesServices,
		IGameService gameServices) {
		_categoriesService = categoriesService;
		_devicesService = devicesServices;
		_gameService = gameServices;
	}

	public IActionResult Index() {
		var games = _gameService.GetAll();
		return View(games);
	}

	[HttpGet]
	public IActionResult Create() {
		CreateGameFormViewModel viewModel = new() {
			Categories = _categoriesService.GetSelectedList(),
			Devices = _devicesService.GetSelectedList()
		};
		return View(viewModel);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Create(CreateGameFormViewModel model) {
		if (!ModelState.IsValid) {
			model.Categories = _categoriesService.GetSelectedList();
			model.Devices = _devicesService.GetSelectedList();

			return View(model);
		}

		await _gameService.Create(model);

		return RedirectToAction(nameof(Index));
	}
}