using UnoGame.Models.Enum;
using UnoGame.Models.Interface;

namespace UnoGame.Models;

public class PlayerData
{
	public IPlayer Player { get; }
	public List<ICard> CardsInHand { get; } = [];
	public List<ICard> GetCardsInHand => CardsInHand;
	public PlayerStatus PlayerStatus { get; set; }
	public bool PlayerTurn { get; set; }
	private int Points { get; set; }

	public PlayerData(IPlayer player)
	{
		Player = player;
		CardsInHand = [];
		PlayerStatus = PlayerStatus.Inactive;
		PlayerTurn = false;
		Points = 0;
		// PlayerTurn = false;
	}
}
