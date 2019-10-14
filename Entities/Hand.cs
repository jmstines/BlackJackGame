using System;
using System.Collections.Generic;

namespace Entities
{
	public class Hand
	{
		private readonly List<BlackJackCard> cards;
		private readonly List<HandActionTypes> actions;

		public IEnumerable<HandActionTypes> Actions => actions;
		public IEnumerable<BlackJackCard> Cards => cards;
		public int PointValue { get; set; }

		public Hand()
		{
			cards = new List<BlackJackCard>();
			actions = new List<HandActionTypes>
			{
				HandActionTypes.Draw,
				HandActionTypes.Hold,
				HandActionTypes.Split
			};
			PointValue = 0;
		}

		public void AddCard(BlackJackCard card)
		{
			cards.Add(card);
			PointValue = Cards.Value();
		}

		public void UpdateActions()
	}
}
