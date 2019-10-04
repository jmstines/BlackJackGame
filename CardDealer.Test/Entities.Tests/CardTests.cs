using NUnit.Framework;
using System;

namespace Entities.Tests
{
    public class CardTests
    {
        [Test]
        public void NewCard_NullDisplayValue_ArgumentNullException()
        {
			var card = new Card(CardSuit.Clubs, CardRank.Ace);
			Assert.AreEqual(CardSuit.Clubs, card.Suit);
			Assert.AreEqual(CardRank.Ace, card.Rank);
        }
    }
}
