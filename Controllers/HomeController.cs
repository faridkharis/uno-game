using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using UnoGame.Models;

namespace UnoGame.Controllers;

public class HomeController : Controller
{
	private readonly ILogger<HomeController> _logger;

	public HomeController(ILogger<HomeController> logger)
	{
		_logger = logger;
	}

	public IActionResult Index()
	{
		return View();
	}

	[HttpPost]
	public IActionResult StartGame(int numberOfPlayers)
	{
		GameController gameController = new(numberOfPlayers);

		gameController.StartGame(numberOfPlayers);

		var viewModel = new GameViewModel
		{
			PlayerDatas = gameController.PlayerDatas,
			InitialDiscardCard = gameController.discardPile.Last(),
			DiscardCardCount = gameController.discardPile.Count,
			StockPileCount = gameController.stockPile.Count,
			Direction = gameController.currentDirecton,
			PlayerStatus = gameController.playerStatusActive,
			// CurrentPlayerTurn = gameController.
		};

		return View("GameBoard", viewModel);
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}
}
