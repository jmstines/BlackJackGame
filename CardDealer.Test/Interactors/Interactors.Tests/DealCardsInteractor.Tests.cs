using Interactors.Providers;
using Entities;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Interactors.Tests
{
    public class DealCardsInteractorTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void CreateNewDeck_CardCount_52()
        {
            IEnumerable<Card> deck = new CardDeckProvider().Deck;
            var count = deck.ToList().Count;
            Assert.AreEqual(deck.ToList().Count, 52);
        }

        public void CreateNewDeck_2ofClubs_FirstCard()
        {
            IEnumerable<Card> deck = new CardDeckProvider().Deck;
            var card = deck.Single(c => c.Display.Equals("2") && c.Suit.Equals(Suit.Clubs));

            Assert.AreEqual(deck.First(), card);
        }

        public void CreateNewDeck_AceOfSpades_LastCard()
        {
            IEnumerable<Card> deck = new CardDeckProvider().Deck;
            var card = deck.Single(c => c.Display.Equals("A") && c.Suit.Equals(Suit.Spades));

            Assert.AreEqual(deck.Last(), card);
        }
    }
}