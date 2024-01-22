using UnoGame.Models.Enum;

namespace UnoGame.Models.Interface;

public interface ICard
{
  CardColor CardColor { get; }
  CardValue CardValue { get; }
  CardEffect CardEffect { get; }
}
