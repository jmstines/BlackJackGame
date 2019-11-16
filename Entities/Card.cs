using System;
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

        public override bool Equals(object obj) => obj is Card card &&
                   Suit == card.Suit &&
                   Rank == card.Rank;

        public override int GetHashCode() => HashCode.Combine(Suit, Rank);

		public static bool operator ==(Card left, Card right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(Card left, Card right)
		{
			return !(left == right);
		}
	}
}
