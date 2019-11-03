using System;
using System.Collections.Generic;
using Entities;

namespace Interactors.Providers
{
	public class CardProviderRandom : ICardProviderRandom
	{
		private readonly IEnumerable<Card> Deck;

		public Card Card => Deck.RandomItem();
		public CardProviderRandom(Deck deck) => 
			Deck = deck ?? throw new ArgumentNullException(nameof(deck));
	}
}
