using System;

namespace Entities
{
    public struct Card
    {
        public Suit Suit;
        public CardRank Rank;

        public Card(Suit suit, CardRank rank)
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
