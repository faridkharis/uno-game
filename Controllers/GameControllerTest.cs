using UnoGame.Models.Interface;
using UnoGame.Models.Enum;
using UnoGame.Models;

namespace UnoGame.Controllers;

public class GameControllerTest
{
  private Dictionary<IPlayer, PlayerData> PlayerData { get; }
  private List<IPlayer> _Players { get; }
  private List<ICard>? _Cards { get; }
  public PlayerStatus PlayerStatus { get; private set; }
  public Direction Direction { get; private set; }
  public CardValue CardValue { get; private set; }
  public CardEffect CardEffect { get; private set; }
  private int MaxPlayers { get; }
  public Action<Direction>? OnDirectionChange;
  public Action<IPlayer, ICard>? OnPlayedCard;
  // private int maxCardInHand = 7;
  // private PlayerData? playerData;

  public GameControllerTest(int maxPlayers, List<ICard> cards)
  {
    MaxPlayers = maxPlayers;
    PlayerData = new Dictionary<IPlayer, PlayerData>();
    _Cards = new List<ICard>();
    _Players = new List<IPlayer>();
    Direction = Direction.Clockwise;
  }

  public void AddPlayer(IPlayer player) { }
  public bool StartGame()
  {
    return false;
  }
  public void DistributeCard(int maxCardInHand) { }
  public Deck GetDeck()
  {
    Deck deck = new();
    return deck;
  }
  public List<IPlayer> GetPlayers()
  {
    return [];
  }
  public PlayerStatus GetPlayerStatus(IPlayer player)
  {
    return PlayerStatus.Active;
  }
  public List<ICard> GetPlayerCard(IPlayer Player)
  {
    return [];
  }
  public int GetPlayerPoint(IPlayer player)
  {
    return 0;
  }
  public bool PlayCard(IPlayer currentplayer, ICard card)

  {
    return false;
  }
  // public ICard GetCardOnTop()
  // {
  // 	Card card = new();
  // 	return card;
  // }
  public CardValue CheckCardValue(IPlayer currentplayer, ICard card)
  {
    return CardValue.Skip;
  }
  public bool CheckCard(IPlayer currentplayer, ICard currentplayercard)
  {
    return false;
  }
  public bool ApplyEffectToPlayer(IPlayer nextplayer, CardEffect cardeffect)
  {
    return false;
  }
  public IPlayer NextPlayer()
  {
    Player player = new(1, "player");
    return player;
  }
  // public ICard DrawCard(IPlayer player)
  // {
  // 	Card card = new();
  // 	return card;
  // }
  public List<ICard> DrawCard(IPlayer player, int numberofcard)
  {
    return [];
  }
  public void ChangeDirection()
  {
    Direction = (Direction == Direction.Clockwise) ? Direction.CounterClockwise : Direction.Clockwise;

    OnDirectionChange?.Invoke(Direction);
  }
  public Dictionary<IPlayer, int> GetPlayersSummary()
  {
    return [];
  }
  public bool NextRound()
  {
    return false;
  }
  public IPlayer GetWinner()
  {
    Player player = new(1, "player");
    return player;
  }
  public bool EndGame()
  {
    return false;
  }
}
