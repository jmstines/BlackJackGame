using Entities.Enums;
using Entities.Interfaces;
using NUnit.Framework;
using System.Collections.Generic;

namespace Entities.Tests
{
	class HandValueTests
	{
		[Test]
		public void AceAndTen_GetValue_ValueEqualsBlackJack()
		{
			var cards = new List<IBlackJackCard>();
			var aceSpades = new Card(CardSuit.Spades, CardRank.Ace);
			var tenSpades = new Card(CardSuit.Spades, CardRank.Ten);
			cards.Add(new BlackJackCard(aceSpades, false));
			cards.Add(new BlackJackCard(tenSpades, false));

			Assert.AreEqual(BlackJackConstants.BlackJack, HandValue.GetValue(cards));
		}

		[Test]
		public void AceTenTen_GetValue_ValueEqualsBlackJack()
		{
			var cards = new List<IBlackJackCard>();
			var aceSpades = new Card(CardSuit.Spades, CardRank.Ace);
			var tenSpades = new Card(CardSuit.Spades, CardRank.Ten);
			cards.Add(new BlackJackCard(aceSpades, false));
			cards.Add(new BlackJackCard(tenSpades, false));
			cards.Add(new BlackJackCard(tenSpades, false));

			Assert.AreEqual(BlackJackConstants.BlackJack, HandValue.GetValue(cards));
		}

		[Test]
		public void AceAceTen_GetValue_ValueEqualsTwelve()
		{
			var cards = new List<IBlackJackCard>();
			var aceSpades = new Card(CardSuit.Spades, CardRank.Ace);
			var tenSpades = new Card(CardSuit.Spades, CardRank.Ten);
			cards.Add(new BlackJackCard(aceSpades, false));
			cards.Add(new BlackJackCard(aceSpades, false));
			cards.Add(new BlackJackCard(tenSpades, false));

			Assert.AreEqual(12, HandValue.GetValue(cards));
		}
	}
}
