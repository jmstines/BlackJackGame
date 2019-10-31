using System;
using System.Collections.Generic;
using System.Linq;

namespace Entities
{
	public class Hand
	{
		private readonly List<BlackJackCard> cards;

		public IEnumerable<HandActionTypes> Actions => HandActions.GetActions(IsBust, cards.Count);
		public IEnumerable<BlackJackCard> Cards => cards;
		public int PointValue => HandValue.GetValue(cards);
		public bool IsBust => PointValue > BlackJackConstants.BlackJack;

		public Hand()
		{
			cards = new List<BlackJackCard>();	
		}

		public void AddCard(BlackJackCard card)
		{
			cards.Add(card);
		}
	}
}
