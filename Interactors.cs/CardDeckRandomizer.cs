using Entities;
using Interactors.Providers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Interactors
{
    public class CardDeckRandomizer
    {
        private readonly IRandomProvider Random;
        private List<Card> CurrentDeck;

        public CardDeckRandomizer(IRandomProvider random)
          => Random = random ?? throw new ArgumentNullException(nameof(random));

        public IEnumerable<Card> Shuffle(IEnumerable<Card> cardDeck, uint numberOfTimes = 1)
        {
            CurrentDeck = cardDeck?.ToList() ?? throw new ArgumentNullException(nameof(cardDeck));

            for (uint i = 0; i < numberOfTimes; i++)
            {
                SingleDeckShuffle();
            }
            return CurrentDeck;
        }

        private void SingleDeckShuffle()
        {
            List<Card> TempDeck = new List<Card>();
            int count = CurrentDeck.Count();
            while (count > 0)
            {
                int cardIndex = Random.Next(0, count - 1);
                Card currentCard = CurrentDeck.ElementAt(cardIndex);
                CurrentDeck.Remove(currentCard);
                TempDeck.Add(currentCard);
                count--;
            }
            CurrentDeck = TempDeck;
        }
    }
}
