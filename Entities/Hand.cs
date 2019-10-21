using System;
using System.Collections.Generic;
using System.Linq;

namespace Entities
{
	public class Hand
	{
		private readonly List<BlackJackCard> cards;
		private List<HandActionTypes> actions;

		public IEnumerable<HandActionTypes> Actions => actions;
		public IEnumerable<BlackJackCard> Cards => cards;
		public int PointValue { get; set; }

		public Hand()
		{
			cards = new List<BlackJackCard>();
			actions = new List<HandActionTypes>
			{
				HandActionTypes.Draw,
				HandActionTypes.Hold
			};
			PointValue = 0;
		}

		public void AddCard(BlackJackCard card)
		{
			cards.Add(card);
			PointValue = Cards.Value();
		}

		public void SetActions(List<HandActionTypes> actions)
		{
			this.actions = actions;
		}
	}
}
