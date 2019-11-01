using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
	public class CardValue
	{
		private CardValue() { }

		public static int GetValue(CardRank rank)
		{
			int value;
			switch (rank)
			{
				case CardRank.Ace:
					value = 11;
					break;
				case CardRank.Two:
					value = 2;
					break;
				case CardRank.Three:
					value = 3;
					break;
				case CardRank.Four:
					value = 4;
					break;
				case CardRank.Five:
					value = 5;
					break;
				case CardRank.Six:
					value = 6;
					break;
				case CardRank.Seven:
					value = 7;
					break;
				case CardRank.Eight:
					value = 8;
					break;
				case CardRank.Nine:
					value = 9;
					break;
				default:
					value = 10;
					break;
			}
			return value;
		}
	}
}
