using System;

namespace Entities
{
	public struct BlackJackCard
	{
		public CardSuit Suit { get; private set; }
		public CardRank Rank { get; private set; }
		public bool FaceDown { get; private set; }

		public BlackJackCard(Card card, bool faceDown)
        {
			Suit = card.Suit != 0 ? card.Suit : throw new ArgumentOutOfRangeException(nameof(card.Suit));
            Rank = card.Rank != 0 ? card.Rank : throw new ArgumentOutOfRangeException(nameof(card.Suit));
            FaceDown = faceDown;
        }

		public override bool Equals(object obj)
        {
            return obj is BlackJackCard card &&
                   Suit == card.Suit &&
                   Rank == card.Rank;
        }

		public override int GetHashCode()
        {
            return HashCode.Combine(Suit, Rank);
        }
    }
}
