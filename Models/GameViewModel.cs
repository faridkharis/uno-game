using UnoGame.Models.Enum;
using UnoGame.Models.Interface;

namespace UnoGame.Models;

public class GameViewModel
{
	public required Dictionary<IPlayer, PlayerData> PlayerDatas { get; set; }
	public required ICard InitialDiscardCard { get; set; }
	public int DiscardCardCount { get; set; }
	public int StockPileCount { get; set; }
	public Direction Direction { get; set; }
	public PlayerStatus PlayerStatus { get; set; }
	// public required PlayerData CurrentPlayerTurn { get; set; }
}
