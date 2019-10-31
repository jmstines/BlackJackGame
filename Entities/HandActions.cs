using System.Collections.Generic;

namespace Entities
{
	public static class HandActions
	{
		public static IEnumerable<HandActionTypes> GetActions(bool isBust, int cardCount)
		{
			var Actions = new List<HandActionTypes>();
			if(isBust)
			{
				Actions.Add(HandActionTypes.Pass);
			}
			else if(cardCount <= 2)
			{
				Actions.Add(HandActionTypes.Draw);
				Actions.Add(HandActionTypes.Hold);
			}
			return Actions;
		}
	}
}
