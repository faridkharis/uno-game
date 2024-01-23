using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using UnoGame.Models;

namespace UnoGame.Controllers;

public class HomeController : Controller
{
	private readonly GameController _gameController;
	private readonly ILogger<HomeController> _logger;
	public HomeController(ILogger<HomeController> logger)
	{
		_logger = logger;
		_gameController = new GameController();
	}

	public IActionResult Index()
	{
		return View();
	}

	[HttpPost]
	public IActionResult StartGame(int numberOfPlayers)
	{
		// GameController gameController = new();
		_gameController.StartGame(numberOfPlayers);

		var viewModel = new GameViewModel
		{
			PlayerDatas = _gameController.PlayersData,
			InitialDiscardCard = _gameController.discardPile.Last(),
			DiscardCardCount = _gameController.discardPile.Count,
			StockPileCount = _gameController.stockPile.Count,
			Direction = _gameController.currentDirecton,
			PlayerStatus = _gameController.playerStatusActive,
		};

		return View("GameBoard", viewModel);
	}



	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}
}
