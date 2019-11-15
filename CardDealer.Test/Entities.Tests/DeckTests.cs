using Entities.Interfaces;
using Interactors.Providers;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Entities.Tests
{
	class DeckTests
	{
		[Test]
		public void	CreateNewDeck_()
		{
			IEnumerable<ICard> deck = new Deck();
			Card TwoClubs = new Card(CardSuit.Clubs, CardRank.Two);
			Card AceSpades = new Card(CardSuit.Spades, CardRank.Ace);
			Assert.AreEqual(52, deck.Count());
			Assert.AreEqual(TwoClubs, deck.First());
			Assert.AreEqual(AceSpades, deck.Last());
		}
	}
}
