using System.Collections.Generic;
using System.Linq;

namespace Entities
{
	public class HandValue
	{
		private HandValue() { }

		public static int GetValue(IEnumerable<BlackJackCard> cards)
		{
			int value = cards.Sum(c => CardValue.GetValue(c.Rank));
			var aceCount = cards.Count(c => c.Rank.Equals(CardRank.Ace));
			for (int i = 0; i < aceCount; i++)
			{
				value = value > BlackJackConstants.BlackJack ? value - 10 : value;
			}
			return value;
		}
	}
}
