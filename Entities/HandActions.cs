using Entities.Enums;
using Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Entities
{
	public static class HandActions
	{
		public static IEnumerable<HandActionTypes> GetActions(IEnumerable<IBlackJackCard> cards)
		{
			_ = cards?.ToList() ?? throw new ArgumentNullException(nameof(cards));
			var actions = new List<HandActionTypes>();
			if (HandValue.GetValue(cards) >= BlackJackConstants.BlackJack)
			{
				actions.Add(HandActionTypes.Pass);
			}
			else
			{
				if (AllowSplit(cards))
				{
					actions.Add(HandActionTypes.Split);
				}
				actions.Add(HandActionTypes.Draw);
				actions.Add(HandActionTypes.Hold);
			}
			return actions;
		}

		private static bool AllowSplit(IEnumerable<IBlackJackCard> cards)
		{
			return cards.Count() == 2 && cards.All(c => c.Value == 10);
		}
	}
}
