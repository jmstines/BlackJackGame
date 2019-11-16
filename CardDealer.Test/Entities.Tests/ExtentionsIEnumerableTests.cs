using Entities.Interfaces;
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
			IEnumerable<ICard> deck = new Deck();
			IEnumerable<ICard> deckSuffled = deck.Shuffle(new RandomProvider());

			Assert.AreNotEqual(deck.ToList(), deckSuffled.ToList());
			Assert.AreNotEqual(deck.First(), deckSuffled.First());
			Assert.AreEqual(deck.Count(), deckSuffled.Count());
		}
	}
}
