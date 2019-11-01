using System.Collections.Generic;
using System.Linq;

namespace Entities
{
	public static class HandActions
	{
		public static IEnumerable<HandActionTypes> GetActions(HandStatusTypes status, IEnumerable<BlackJackCard> cards)
		{
			var Actions = new List<HandActionTypes>();
			if(status.Equals(HandStatusTypes.Bust))
			{
				Actions.Add(HandActionTypes.Pass);
			}
			else if(cards.Count() <= 2 && AllowSplit(cards))
			{
				Actions.Add(HandActionTypes.Draw);
				Actions.Add(HandActionTypes.Hold);
				Actions.Add(HandActionTypes.Split);
			}
			else
			{
				Actions.Add(HandActionTypes.Draw);
				Actions.Add(HandActionTypes.Hold);
			}
			return Actions;
		}

		private static bool AllowSplit(IEnumerable<BlackJackCard> cards)
		{
			return cards.Count() == 2 && cards.All(c => CardValue.GetValue(c.Rank) == 10);
		}
	}
}
