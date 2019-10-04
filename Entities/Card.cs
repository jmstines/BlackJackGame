using System;

namespace Entities
{
    public struct Card
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
    }
}
