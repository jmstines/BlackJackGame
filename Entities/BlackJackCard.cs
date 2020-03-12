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

			switch (Rank)
			{
				case CardRank.Ace:
					Value = BlackJackConstants.AceHighValue;
					break;
				case CardRank.Two:
					Value = 2;
					break;
				case CardRank.Three:
					Value = 3;
					break;
				case CardRank.Four:
					Value = 4;
					break;
				case CardRank.Five:
					Value = 5;
					break;
				case CardRank.Six:
					Value = 6;
					break;
				case CardRank.Seven:
					Value = 7;
					break;
				case CardRank.Eight:
					Value = 8;
					break;
				case CardRank.Nine:
					Value = 9;
					break;
				case CardRank.Ten:
					Value = BlackJackConstants.DefaultCardValue;
					break;
				case CardRank.Jack:
					Value = BlackJackConstants.DefaultCardValue;
					break;
				case CardRank.Queen:
					Value = BlackJackConstants.DefaultCardValue;
					break;
				case CardRank.King:
					Value = BlackJackConstants.DefaultCardValue;
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(card.Rank), "Card Rank must be 2 through Ace.");
			}
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
