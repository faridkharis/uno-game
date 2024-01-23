using UnoGame.Models.Enum;

namespace UnoGame.Models.Interface;

public interface ICard
{
	int CardId { get; }
	CardColor CardColor { get; }
	CardValue CardValue { get; }
	CardEffect CardEffect { get; }
}
