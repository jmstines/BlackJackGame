using Entities.Enums;
using Entities.Interfaces;

namespace Entities
{
	public struct Card : ICard
	{
		public CardSuit Suit { get; private set; }
		public CardRank Rank { get; private set; }

		public Card(CardSuit suit, CardRank rank)
		{
			Suit = suit;
			Rank = rank;
		}
	}
}
