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
			Value = 0;
			SetCardValue();
		}

		private void SetCardValue()
		{
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
				default:
					Value = BlackJackConstants.DefaultCardValue;
					break;
			}
		}

		public override bool Equals(object obj)
		{
			return obj is BlackJackCard card &&
				   Suit == card.Suit &&
				   Rank == card.Rank &&
				   FaceDown == card.FaceDown &&
				   Value == card.Value;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(Suit, Rank, FaceDown, Value);
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
