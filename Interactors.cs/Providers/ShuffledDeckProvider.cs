using System;
using System.Collections.Generic;
using System.Linq;
using Entities;

namespace Interactors.Providers
{
    public class ShuffledDeckProvider : IShuffledDeckProvider
    {
        private List<Card> SourceDeck;
        private readonly IRandomProvider Random;

        public ShuffledDeckProvider(IEnumerable<Card> deck, IRandomProvider random)
        {
            SourceDeck = new List<Card>(deck) ?? throw new ArgumentNullException(nameof(deck));
            Random = random ?? throw new ArgumentNullException(nameof(random));
        }

        public IEnumerable<Card> Shuffle()
        {
            List<Card> ShuffledDeck = new List<Card>();
            while (SourceDeck.Any())
            {
                Card currentCard = SourceDeck.ElementAt(GetCardIndex());
                ShuffledDeck.Add(currentCard);
                SourceDeck.Remove(currentCard);
            }
            SourceDeck = ShuffledDeck;
            return ShuffledDeck;
        }

        private int GetCardIndex() => Random.Next(minValue: 0, maxValue: SourceDeck.Count);
    }
}
