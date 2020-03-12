using Entities.Interfaces;
using System;
using System.Collections.Generic;

namespace Entities.Providers
{
	public class CardProvider : CardProviderBase, ICardProvider
	{
		public override IEnumerable<ICard> Deck { get; set; }
		public override IRandomProvider RandomProvider { get; set; }

		public IEnumerable<ICard> Cards(int count) => RandomCards(count);

		public CardProvider(IRandomProvider randomProvider, Deck deck)
		{
			Deck = deck ?? throw new ArgumentNullException(nameof(deck));
			RandomProvider = randomProvider ?? throw new ArgumentNullException(nameof(randomProvider));
		}
	}
}
