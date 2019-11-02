using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
	public class CardValue
	{
		private readonly CardRank Rank;
		public int Value { get; private set; } = 0;
		public CardValue(CardRank rank) {
			Rank = rank.Equals(0) ? throw new ArgumentNullException(nameof(rank)) : rank;
			GetValue();
		}

		private void GetValue()
		{
			switch (Rank)
			{
				case CardRank.Ace:
					Value = 11;
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
					Value = 10;
					break;
			}
		}
	}
}
