using UnoGame.Models.Enum;
using UnoGame.Models.Interface;

namespace UnoGame.Models;

public class PlayerData
{
	public IPlayer Player { get; }
	public List<ICard> CardsInHand { get; } = [];
	public PlayerStatus PlayerStatus { get; set; }
	private int Points { get; set; }
	public bool PlayerTurn { get; set; }

	public PlayerData(IPlayer player)
	{
		Player = player;
		CardsInHand = [];
		PlayerStatus = PlayerStatus.Inactive;
		Points = 0;
		PlayerTurn = false;
	}
}
