using System;
using System.Collections.Generic;
using System.Linq;

namespace Entities
{
	public class HandActions
	{
		private readonly List<BlackJackCard> Cards;
		private readonly HandStatusTypes Status;
		private List<HandActionTypes> actions;

		public IEnumerable<HandActionTypes> Actions => actions;
		public HandActions(HandStatusTypes status, IEnumerable<BlackJackCard> cards)
		{
			Cards = cards?.ToList() ?? throw new ArgumentNullException(nameof(cards));
			Status = status.Equals(0) ? throw new ArgumentOutOfRangeException(nameof(status)) : status;
			GetActions();
		}

		private void GetActions()
		{
			actions = new List<HandActionTypes>();
			if(Status.Equals(HandStatusTypes.Bust))
			{
				actions.Add(HandActionTypes.Pass);
			}
			else if(Cards.Count == 2 && AllowSplit())
			{
				actions.Add(HandActionTypes.Draw);
				actions.Add(HandActionTypes.Hold);
				actions.Add(HandActionTypes.Split);
			}
			else
			{
				actions.Add(HandActionTypes.Draw);
				actions.Add(HandActionTypes.Hold);
			}
		}

		private bool AllowSplit()
		{
			return Cards.Count == 2 && Cards.All(c => new CardValue(c.Rank).Value == 10);
		}
	}
}
