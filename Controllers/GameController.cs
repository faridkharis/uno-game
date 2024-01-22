using UnoGame.Models.Interface;
using UnoGame.Models.Enum;
using UnoGame.Models;

namespace UnoGame.Controllers;

public class GameController
{
	// Properties
	private int NumberOfPlayers { get; }
	public List<ICard> Cards { get; }
	private List<IPlayer> Players { get; }
	public Dictionary<IPlayer, PlayerData> PlayerDatas { get; }
	public Direction Direction { get; private set; }
	public List<ICard> stockPile = [];
	public List<ICard> discardPile = [];
	public PlayerStatus playerStatusActive = PlayerStatus.Active;
	public Direction currentDirecton = Direction.Clockwise;

	// Constructor
	public GameController(int numberOfPlayers)
	{
		NumberOfPlayers = numberOfPlayers;
		PlayerDatas = new Dictionary<IPlayer, PlayerData>();
		Cards = [];
		Players = [];
		// Direction = Direction.Clockwise;
	}

	// Methods
	public void StartGame(int numberOfPlayers)
	{
		AddPlayers(numberOfPlayers);
		GenerateCards();
		ShuffleCards();
		DistributeCards();
		AddStockPile();
		DrawInitialCard();
	}
	public void AddPlayers(int numberOfPlayers)
	{
		for (int i = 1; i <= numberOfPlayers; i++)
		{
			IPlayer player = new Player(i, $"Player {i}");
			PlayerData playerData = new(player);
			PlayerDatas.Add(player, playerData);
		}

		// Set the first player's status to Active and others to Inactive

		// var firstPlayer = PlayerDatas.Values.ToList();
		// firstPlayer[random.Next(firstPlayer.Count)].PlayerStatus = PlayerStatus.Active;

		Random random = new();
		int index = random.Next(PlayerDatas.Count);
		var firstPlayer = PlayerDatas.ElementAt(index).Value;
		firstPlayer.PlayerStatus = PlayerStatus.Active;

		foreach (var playerData in PlayerDatas.Values.Where(p => p != firstPlayer))
		{
			playerData.PlayerStatus = PlayerStatus.Inactive;
		}

	}
	public List<ICard> GenerateCards()
	{
		foreach (CardColor color in Enum.GetValues(typeof(CardColor)))
		{
			if (color != CardColor.Black) // Skip Blask (used for special wild cards)
			{
				for (CardValue value = CardValue.Zero; value <= CardValue.DrawTwo; value++)
				{
					CardEffect effect = CardEffect.None;
					Cards.Add(new Card(color, value, effect));
					if (value != CardValue.Zero) // Skip Zero (one zero card each color)
					{
						Cards.Add(new Card(color, value, effect));
					}
				}
			}
		}

		// Adding Wild and Wild Draw Four cards
		for (int i = 0; i < 4; i++)
		{
			Cards.Add(new Card(CardColor.Black, CardValue.Wild, CardEffect.Wild));
			Cards.Add(new Card(CardColor.Black, CardValue.WildDrawFour, CardEffect.WildDrawFour));
		}

		return Cards;
	}
	public void ShuffleCards()
	{
		Random random = new();
		int n = Cards.Count;

		while (n > 1)
		{
			n--;
			int k = random.Next(n + 1);
			(Cards[n], Cards[k]) = (Cards[k], Cards[n]);
		}
	}
	public void DistributeCards()
	{
		foreach (var playerData in PlayerDatas)
		{
			// Distribute seven cards to each player
			for (int i = 0; i < 7; i++)
			{
				ICard card = Cards.First();
				Cards.RemoveAt(0);
				playerData.Value.CardsInHand.Add(card);
			}
		}
	}
	public void AddStockPile()
	{
		stockPile = Cards;
	}
	public void DrawInitialCard()
	{
		if (stockPile.Count <= 0)
		{
			ReshuffleDiscardPile();
		}
		else
		{
			discardPile.Add(Cards[0]);
			stockPile.RemoveAt(0);
		}
	}
	public ICard DrawCardFromStockPile()
	{
		if (stockPile.Count == 0)
		{
			// Reshuffle discard pile into stock pile if stock pile is empty
			ReshuffleDiscardPile();
		}

		// Draw a card from the stock pile
		ICard drawnCard = stockPile[0];
		stockPile.RemoveAt(0);

		return drawnCard;
	}
	private void ReshuffleDiscardPile()
	{
		// Move cards from the discard pile back to the stock pile
		stockPile.AddRange(discardPile);
		discardPile.Clear();

		// Shuffle the stock pile
		ShuffleCards();
	}

}
