using Interactors.Providers;
using NUnit.Framework;
using System;
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

		[Test]
		public void AceAndTen_GetValue_ValueEqualsBlackJack()
		{
			var deck = new CardDeckProvider().Deck;
			var cards = new List<BlackJackCard>();
			var aceSpades = deck.Where(c => c.Rank.Equals(CardRank.Ace) && c.Suit.Equals(CardSuit.Spades)).Single();
			var tenSpades = deck.Where(c => c.Rank.Equals(CardRank.Ten) && c.Suit.Equals(CardSuit.Spades)).Single();
			cards.Add(new BlackJackCard(aceSpades, false));
			cards.Add(new BlackJackCard(tenSpades, false));

			Assert.AreEqual(BlackJackConstants.BlackJack, cards.Value());
		}

		[Test]
		public void AceTenTen_GetValue_ValueEqualsBlackJack()
		{
			var deck = new CardDeckProvider().Deck;
			var cards = new List<BlackJackCard>();
			var aceSpades = deck.Where(c => c.Rank.Equals(CardRank.Ace) && c.Suit.Equals(CardSuit.Spades)).Single();
			var tenSpades = deck.Where(c => c.Rank.Equals(CardRank.Ten) && c.Suit.Equals(CardSuit.Spades)).Single();
			cards.Add(new BlackJackCard(aceSpades, false));
			cards.Add(new BlackJackCard(tenSpades, false));
			cards.Add(new BlackJackCard(tenSpades, false));

			Assert.AreEqual(BlackJackConstants.BlackJack, cards.Value());
		}

		[Test]
		public void AceAceTen_GetValue_ValueEqualsTwelve()
		{
			var deck = new CardDeckProvider().Deck;
			var cards = new List<BlackJackCard>();
			var aceSpades = deck.Where(c => c.Rank.Equals(CardRank.Ace) && c.Suit.Equals(CardSuit.Spades)).Single();
			var tenSpades = deck.Where(c => c.Rank.Equals(CardRank.Ten) && c.Suit.Equals(CardSuit.Spades)).Single();
			cards.Add(new BlackJackCard(aceSpades, false));
			cards.Add(new BlackJackCard(aceSpades, false));
			cards.Add(new BlackJackCard(tenSpades, false));

			Assert.AreEqual(12, cards.Value());
		}


	}
}
