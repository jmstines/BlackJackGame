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
		public int Value { get; private set; }

		public BlackJackCard(ICard card, bool faceDown)
		{
			Suit = card.Suit != 0 ? card.Suit : throw new ArgumentOutOfRangeException(nameof(card.Suit));
			Rank = card.Rank != 0 ? card.Rank : throw new ArgumentOutOfRangeException(nameof(card.Rank));
			FaceDown = faceDown;

			Value = Rank switch
			{
				CardRank.Ace => BlackJackConstants.AceHighValue,
				CardRank.Two => 2,
				CardRank.Three => 3,
				CardRank.Four => 4,
				CardRank.Five => 5,
				CardRank.Six => 6,
				CardRank.Seven => 7,
				CardRank.Eight => 8,
				CardRank.Nine => 9,
				CardRank.Ten => BlackJackConstants.DefaultCardValue,
				CardRank.Jack => BlackJackConstants.DefaultCardValue,
				CardRank.Queen => BlackJackConstants.DefaultCardValue,
				CardRank.King => BlackJackConstants.DefaultCardValue,
				_ => throw new ArgumentOutOfRangeException(nameof(card.Rank), "Card Rank must be 2 through Ace."),
			};
		}

		public override bool Equals(object obj) => obj is BlackJackCard card &&
				   Suit == card.Suit &&
				   Rank == card.Rank;

		public override int GetHashCode() => HashCode.Combine(Suit, Rank);

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
