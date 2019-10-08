using Interactors.Providers;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Entities.Tests
{
	class DeckShufflerTests
    {
		[Test]
		public void CreateNewDeck_NullDeck_ArgumentNullException()
		{
			Assert.Throws<ArgumentNullException>(() => DeckShuffler.Shuffle(null));
		}

		[Test]
        public void CreateNewDeck_Suffle_CardsShuffled()
        {
            IEnumerable<Card> deck = new CardDeckProvider().Deck;
            IEnumerable<Card> deckSuffled = DeckShuffler.Shuffle(deck);

            Assert.AreNotEqual(deck, deckSuffled);
        }
    }
}
