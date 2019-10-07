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
