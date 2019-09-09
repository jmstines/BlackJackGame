using Interactors.Mocks;
using Interactors.Providers;
using Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Interactors.Tests
{
    class ShuffledDeckProviderTests
    {
        [Test]
        public void CreateNewDeck_Suffle_CardsShuffled()
        {
            var random = new RandomProvider((int)DateTime.UtcNow.Ticks);
            IEnumerable<Card> deck = new CardDeckProvider().Deck;
            IEnumerable<Card> deckSuffled = new ShuffledDeckProvider(deck, random).Shuffle();

            Assert.AreNotEqual(deck, deckSuffled);
        }

        [Test]
        public void CreateNewDeck_Suffle_CardsReversed()
        {
            IEnumerable<Card> deck = new CardDeckProvider().Deck;
            var random = new RandomProviderMock(deck.Count());
            IEnumerable<Card> deckSuffled = new ShuffledDeckProvider(deck, random).Shuffle();
            Assert.AreEqual(deck.Reverse(), deckSuffled);
        }
    }
}
