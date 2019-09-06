using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities;

namespace Interactors.Providers
{
	public class ShuffledDeckProvider : IShuffledDeckProvider
	{
		public List<Card> ShuffledDeck { get; private set; }
		private readonly List<Card> Deck;
		private readonly IRandomProvider Random;

		public ShuffledDeckProvider(List<Card> deck, IRandomProvider random)
		{
			Deck = deck ?? throw new ArgumentNullException(nameof(deck));
			Random = random ?? throw new ArgumentNullException(nameof(random));
			Shuffle();
		}

		private void Shuffle()
		{
			var tempDeck = new List<Card>();
			int count = Deck.Count;
			int firstCardIndex = 0;
			while (count > 0)
			{
				int cardIndex = Random.Next(firstCardIndex, count);
				Card currentCard = Deck.ElementAt(cardIndex);
				tempDeck.Add(currentCard);
				Deck.Remove(currentCard);
				count--;
			}
			ShuffledDeck = tempDeck;
		}
	}
}
