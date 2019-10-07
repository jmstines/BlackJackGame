using System;
using System.Collections.Generic;
using System.Linq;
using Entities;

namespace Interactors.Providers
{
    public class DeckShuffler
    {
        private static List<Card> SourceDeck;
        private static Random Random;

		private DeckShuffler() { }

        public static IEnumerable<Card> Shuffle(IEnumerable<Card> deck)
        {
			SourceDeck = new List<Card>(deck) ?? throw new ArgumentNullException(nameof(deck));
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

        private static int GetCardIndex() => Random.Next(minValue: 0, maxValue: SourceDeck.Count);
    }
}
