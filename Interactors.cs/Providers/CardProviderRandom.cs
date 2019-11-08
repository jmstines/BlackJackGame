using System;
using System.Collections.Generic;
using System.Linq;
using Entities;

namespace Interactors.Providers
{
	public class CardProviderRandom : ICardProviderRandom
	{
		private readonly IEnumerable<Card> Deck;

		public IEnumerable<Card> Cards(int count) => RandomCards(count);

		public CardProviderRandom(Deck deck) =>
			Deck = deck ?? throw new ArgumentNullException(nameof(deck));

		private IEnumerable<Card> RandomCards(int count)
		{
			var source = new List<Card>(Deck);
			var Random = new Random((int)DateTime.UtcNow.Ticks);
			var shuffled = new List<Card>();
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
