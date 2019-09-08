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

        public CardDeckRandomizer(IRandomProvider random)
          => Random = random ?? throw new ArgumentNullException(nameof(random));

        public List<Card> Shuffle(List<Card> cardDeck, int numberOfTimes = 1)
        {
            if (cardDeck == null)
            {
                throw new ArgumentNullException(nameof(cardDeck));
            }
            var CurrentDeck = new List<Card>(cardDeck);
            for (int i = 0; i < numberOfTimes; i++)
            {
                var TempDeck = new List<Card>();
                int count = CurrentDeck.Count;
                while (count > 0)
                {
                    int cardIndex = Random.Next(0, count - 1);
                    Card currentCard = CurrentDeck.ElementAt(cardIndex);
                    TempDeck.Add(currentCard);
                    CurrentDeck.Remove(currentCard);
                    count--;
                }
                CurrentDeck = TempDeck;
            }
            return CurrentDeck;
        }
    }
}
