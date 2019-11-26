using Entities.Enums;
using Entities.Interfaces;
using System;

namespace Entities
{
	public struct BlackJackCard : IBlackJackCard
	{
		public CardSuit Suit { get; private set; }
		public CardRank Rank { get; private set; }
		public bool FaceDown { get; private set; }

		public BlackJackCard(ICard card, bool faceDown)
		{
			Suit = card.Suit != 0 ? card.Suit : throw new ArgumentOutOfRangeException(nameof(card.Suit));
			Rank = card.Rank != 0 ? card.Rank : throw new ArgumentOutOfRangeException(nameof(card.Rank));
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

		public static bool operator ==(BlackJackCard left, BlackJackCard right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(BlackJackCard left, BlackJackCard right)
		{
			return !(left == right);
		}
	}
}
