using Interactors.Providers;
using Entities;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Interactors.Tests
{
    public class CardDeckProviderTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void CreateNewDeck_CardCount_52()
        {
            IEnumerable<Card> deck = new CardDeckProvider().Deck;
            Assert.AreEqual(52, deck.Count());
        }

        [Test]
        public void CreateNewDeck_2ofClubs_FirstCard()
        {
            IEnumerable<Card> deck = new CardDeckProvider().Deck;
            var card = deck.Single(c => c.Display.Equals("2") && c.Suit.Equals(Suit.Clubs));

            Assert.AreEqual(deck.First(), card);
        }

        [Test]
        public void CreateNewDeck_AceOfSpades_LastCard()
        {
            IEnumerable<Card> deck = new CardDeckProvider().Deck;
            var card = deck.Single(c => c.Display.Equals("A") && c.Suit.Equals(Suit.Spades));

            Assert.AreEqual(deck.Last(), card);
        }

        [Test]
        public void CreateCustomDeck_AceOfSpades_AllCards()
        {
            IEnumerable<CardDetail> CardDetails = new List<CardDetail>{
                new CardDetail("A", "Ace"), new CardDetail("A", "Ace"),
                new CardDetail("A", "Ace"), new CardDetail("A", "Ace")};

            IEnumerable<Card> deck = new CardDeckProvider(CardDetails).Deck;
            Assert.AreEqual(deck.Count(), 16);
            Assert.AreEqual(deck.Where(c => c.Display.Equals("A")).Count(), 16);
        }
    }
}