using UnoGame.Models.Interface;
using UnoGame.Models.Enum;

namespace UnoGame.Models;

public class Card : ICard
{
	public int Id { get; }
	public CardColor CardColor { get; private set; }
	public CardValue CardValue { get; private set; }
	public CardEffect CardEffect { get; private set; }

	public Card(CardColor cardCcolor, CardValue cardValue, CardEffect cardEffect)
	{
		CardColor = cardCcolor;
		CardValue = cardValue;
		CardEffect = cardEffect;
	}

	public static CardColor GetCardColor()
	{
		return CardColor.Blue;
	}
	public static CardValue GetCardType()
	{
		return CardValue.Skip;
	}
	public static int GetCardValue(CardValue cardValue)
	{
		int valueFromCard = (int)cardValue;
		return valueFromCard;
	}
}
