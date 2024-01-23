using UnoGame.Models.Interface;
using UnoGame.Models.Enum;
using UnoGame.Models;

namespace UnoGame.Controllers;

public class GameController
{
  // Properties
  private List<ICard> Cards { get; }
  private List<IPlayer> Players { get; }
  public Dictionary<IPlayer, PlayerData> PlayersData { get; }
  private Direction Direction { get; set; }

  // Variables
  // private readonly int numberOfPlayers;
  private readonly int maxCardsInHand = 7;
  public List<ICard> stockPile = [];
  public List<ICard> discardPile = [];
  public PlayerStatus playerStatusActive = PlayerStatus.Active;
  public Direction currentDirecton = Direction.Clockwise;

  // Constructor
  public GameController()
  {
    PlayersData = new Dictionary<IPlayer, PlayerData>();
    Cards = [];
    Players = [];
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
      PlayersData.Add(player, playerData);
    }

    // Set the first player's status to Active and others to Inactive
    Random random = new();
    int index = random.Next(PlayersData.Count);
    var firstPlayer = PlayersData.ElementAt(index).Value;
    firstPlayer.PlayerStatus = PlayerStatus.Active;

    foreach (var playerData in PlayersData.Values.Where(p => p != firstPlayer))
    {
      playerData.PlayerStatus = PlayerStatus.Inactive;
    }

  }
  public List<ICard> GenerateCards()
  {
    int cardId = 1;

    foreach (CardColor color in Enum.GetValues(typeof(CardColor)))
    {
      if (color != CardColor.Black) // Skip Black (used for special wild cards)
      {
        for (CardValue value = CardValue.Zero; value <= CardValue.DrawTwo; value++)
        {
          CardEffect effect = CardEffect.None;
          Cards.Add(new Card(cardId++, color, value, effect));
          if (value != CardValue.Zero) // Skip Zero (one zero card each color)
          {
            Cards.Add(new Card(cardId++, color, value, effect));
          }
        }
      }
    }

    // Adding Wild and Wild Draw Four cards
    for (int i = 0; i < 4; i++)
    {
      Cards.Add(new Card(cardId++, CardColor.Black, CardValue.Wild, CardEffect.Wild));
      Cards.Add(new Card(cardId++, CardColor.Black, CardValue.WildDrawFour, CardEffect.WildDrawFour));
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
    foreach (var playerData in PlayersData)
    {
      // Distribute seven cards to each player
      for (int i = 0; i < maxCardsInHand; i++)
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


  // public bool PlayCard(IPlayer player, ICard card)
  // {
  // 	// Check if it's the player's turn
  // 	if (PlayersData.TryGetValue(player, out var playerData) && playerData.PlayerTurn)
  // 	{
  // 		// Check if the card is in the player's hand
  // 		if (playerData.CardsInHand.Contains(card))
  // 		{
  // 			// Check if the card can be played (based on game rules)
  // 			if (CanPlayCard(card))
  // 			{
  // 				// Remove the card from the player's hand and add it to the discard pile
  // 				playerData.CardsInHand.Remove(card);
  // 				discardPile.Add(card);

  // 				// Update the player's turn
  // 				UpdatePlayerTurn();

  // 				return true; // Card played successfully
  // 			}
  // 			else
  // 			{
  // 				// Handle case when the card cannot be played (based on game rules)
  // 				return false;
  // 			}
  // 		}
  // 	}
  // 	return false;
  // }

  public void PlayCard()
  {
    Console.WriteLine("Clicked");
  }


  private void UpdatePlayerTurn()
  {
    // ... logic to update player turn based on game rules (skip, reverse, etc.)
    // You need to implement this based on your game rules.
  }

  private bool CanPlayCard(ICard card)
  {
    // Add your game-specific logic to determine if the card can be played
    // For example, check if the card matches the color or value of the top card in the discard pile
    ICard topDiscard = discardPile.Last();

    if (topDiscard == null)
    {
      // No card in the discard pile, any card can be played
      return true;
    }

    // Implement your rules here
    return card.CardColor == topDiscard.CardColor || card.CardValue == topDiscard.CardValue || card.CardEffect != CardEffect.None;
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
