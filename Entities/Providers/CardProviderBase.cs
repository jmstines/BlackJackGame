using Entities.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Entities.Providers
{
	abstract public class CardProviderBase
	{
		public abstract IEnumerable<ICard> Deck { get; set; }
		public abstract IRandomProvider RandomProvider { get; set; }
		internal IEnumerable<ICard> RandomCards(int count)
		{
			var source = new List<ICard>(Deck);
			var shuffled = new List<ICard>();
			for (int i = 0; i < count; i++)
			{
				var nextIndex = RandomProvider.GetRandom(min: 0, max: source.Count);
				var currentItem = source.ElementAt(nextIndex);
				source.Remove(currentItem);
				shuffled.Add(currentItem);
			}
			return shuffled;
		}
	}
}
