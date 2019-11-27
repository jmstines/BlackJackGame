using Entities.Enums;
using Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Entities
{
	public static class HandValue
	{
		public static int GetValue(IEnumerable<IBlackJackCard> cards)
		{
			_ = cards ?? throw new ArgumentNullException(nameof(cards));

			int value = cards.Sum(c => c.Value);
			var aceCount = cards.Count(c => c.Rank.Equals(CardRank.Ace));
			for (int i = 0; i < aceCount; i++)
			{
				value = value > BlackJackConstants.BlackJack ? value - 10 : value;
			}
			return value;
		}
	}
}
