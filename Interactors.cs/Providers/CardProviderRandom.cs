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

		public IEnumerable<ICard> Cards(int count) => RandomCards(count);

		public CardProviderRandom(Deck deck) =>
			Deck = deck ?? throw new ArgumentNullException(nameof(deck));

		private IEnumerable<ICard> RandomCards(int count)
		{
			var source = new List<ICard>(Deck);
			var Random = new Random((int)DateTime.UtcNow.Ticks);
			var shuffled = new List<ICard>();
			for (int i = 0; i < count; i++)
			{
				var nextIndex = Random.Next(minValue: 0, maxValue: source.Count);
				var currentItem = source.ElementAt(nextIndex);
				source.Remove(currentItem);
				shuffled.Add(currentItem);
			}
			return shuffled;
		}
	}
}
