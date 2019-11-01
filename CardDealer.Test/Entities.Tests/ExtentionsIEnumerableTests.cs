using Interactors.Providers;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Entities.Tests
{
	class ExtentionsIEnumerableTests
	{
		[Test]
		public void CreateNewDeck_Suffle_CardsShuffled()
		{
			IEnumerable<Card> deck = new CardDeckProvider().Deck;
			IEnumerable<Card> deckSuffled = deck.Shuffle();

			Assert.AreNotEqual(deck, deckSuffled);
		}
	}
}
