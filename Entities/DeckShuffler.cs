using System;
using System.Collections.Generic;
using System.Linq;
using Entities;

namespace Interactors.Providers
{
    public class DeckShuffler
    {
        private List<Card> SourceDeck;
        private Random Random;

		public DeckShuffler(IEnumerable<Card> deck)
        {
            SourceDeck = new List<Card>(deck) ?? throw new ArgumentNullException(nameof(deck));
		}

        public IEnumerable<Card> Shuffle()
        {
			Random = new Random((int)DateTime.UtcNow.Ticks);
			var ShuffledDeck = new List<Card>();
            while (SourceDeck.Any())
            {
                var currentCard = SourceDeck.ElementAt(GetCardIndex());
				SourceDeck.Remove(currentCard);
				ShuffledDeck.Add(currentCard);
            }
            SourceDeck = ShuffledDeck;
            return ShuffledDeck;
        }

        private int GetCardIndex() => Random.Next(minValue: 0, maxValue: SourceDeck.Count);
    }
}
