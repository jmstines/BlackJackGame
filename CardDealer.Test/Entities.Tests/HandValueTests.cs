using Entities.Interfaces;
using Interactors.Providers;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Entities.Tests
{
	class HandValueTests
	{
		[Test]
		public void AceAndTen_GetValue_ValueEqualsBlackJack()
		{
			var deck = new CardDeckProvider().Deck;
			var cards = new List<IBlackJackCard>();
			var aceSpades = deck.Where(c => c.Rank.Equals(CardRank.Ace) && c.Suit.Equals(CardSuit.Spades)).Single();
			var tenSpades = deck.Where(c => c.Rank.Equals(CardRank.Ten) && c.Suit.Equals(CardSuit.Spades)).Single();
			cards.Add(new BlackJackCard(aceSpades, false));
			cards.Add(new BlackJackCard(tenSpades, false));

			Assert.AreEqual(BlackJackConstants.BlackJack, HandValue.GetValue(cards));
		}

		[Test]
		public void AceTenTen_GetValue_ValueEqualsBlackJack()
		{
			var deck = new CardDeckProvider().Deck;
			var cards = new List<IBlackJackCard>();
			var aceSpades = deck.Where(c => c.Rank.Equals(CardRank.Ace) && c.Suit.Equals(CardSuit.Spades)).Single();
			var tenSpades = deck.Where(c => c.Rank.Equals(CardRank.Ten) && c.Suit.Equals(CardSuit.Spades)).Single();
			cards.Add(new BlackJackCard(aceSpades, false));
			cards.Add(new BlackJackCard(tenSpades, false));
			cards.Add(new BlackJackCard(tenSpades, false));

			Assert.AreEqual(BlackJackConstants.BlackJack, HandValue.GetValue(cards));
		}

		[Test]
		public void AceAceTen_GetValue_ValueEqualsTwelve()
		{
			var deck = new CardDeckProvider().Deck;
			var cards = new List<IBlackJackCard>();
			var aceSpades = deck.Where(c => c.Rank.Equals(CardRank.Ace) && c.Suit.Equals(CardSuit.Spades)).Single();
			var tenSpades = deck.Where(c => c.Rank.Equals(CardRank.Ten) && c.Suit.Equals(CardSuit.Spades)).Single();
			cards.Add(new BlackJackCard(aceSpades, false));
			cards.Add(new BlackJackCard(aceSpades, false));
			cards.Add(new BlackJackCard(tenSpades, false));

			Assert.AreEqual(12, HandValue.GetValue(cards));
		}
	}
}
