using System;

namespace Entities
{
    public struct Card
    {
		public readonly CardSuit Suit;
        public readonly CardRank Rank;

        public Card(CardSuit suit, CardRank rank)
        {
            Suit = suit;
            Rank = rank;
        }

        public override bool Equals(object obj) => obj is Card card &&
                   Suit == card.Suit &&
                   Rank == card.Rank;

        public override int GetHashCode() => HashCode.Combine(Suit, Rank);
    }
}
