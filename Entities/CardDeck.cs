using System;
using System.Collections.Generic;
using System.Linq;

namespace Entities
{
    public class CardDeck
    {
        public List<Card> Cards;
        private IRandomProvider Random;

        public CardDeck(List<Card> cards)
        {
            Cards = cards;
        }

        public void Shuffle(IRandomProvider random)
        {
            Random = random ?? throw new ArgumentNullException(nameof(random));
            List<Card> ShuffledDeck = new List<Card>();
            while (Cards.Any())
            {
                Card currentCard = Cards.ElementAt(GetCardIndex());
                ShuffledDeck.Add(currentCard);
                Cards.Remove(currentCard);
            }
            Cards = ShuffledDeck;
        }

        private int GetCardIndex() => Random.Next(minValue: 0, maxValue: Cards.Count);
    }
}
