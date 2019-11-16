using System;
using System.Collections.Generic;
using System.Linq;
using Entities;
using Entities.Interfaces;

namespace Interactors.Providers
{
	public class CardProviderRandom : ICardProviderRandom
	{
		private readonly IEnumerable<ICard> Deck;
		private readonly IRandomProvider RandomProvider;

		public IEnumerable<ICard> Cards(int count) => RandomCards(count);

		public CardProviderRandom(IRandomProvider randomProvider, Deck deck)
		{
			Deck = deck ?? throw new ArgumentNullException(nameof(deck));
			RandomProvider = randomProvider ?? throw new ArgumentNullException(nameof(randomProvider));
		}

		private IEnumerable<ICard> RandomCards(int count)
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
