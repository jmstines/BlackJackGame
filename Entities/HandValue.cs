using System;
using System.Collections.Generic;
using System.Linq;

namespace Entities
{
	public class HandValue
	{
		private readonly IEnumerable<BlackJackCard> Cards;
		public int Value { get; private set; }
		public HandValue(IEnumerable<BlackJackCard> cards)
		{
			Cards = cards ?? throw new ArgumentNullException(nameof(cards));
			GetValue();
		}

		public void GetValue()
		{
			Value = Cards.Sum(c => new CardValue(c.Rank).Value);
			var aceCount = Cards.Count(c => c.Rank.Equals(CardRank.Ace));
			for (int i = 0; i < aceCount; i++)
			{
				Value = Value > BlackJackConstants.BlackJack ? Value - 10 : Value;
			}
		}
	}
}
