using NUnit.Framework;
using System;

namespace Entities.Tests
{
	public class BlackJackCardTests
    {
        private readonly bool FaceDown = false;
        private readonly Card Card = new Card(CardSuit.Spades, CardRank.Ace);

		[Test]
        public void NewCard_CardNull_ArgumentNullException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new BlackJackCard(new Card(), FaceDown));
        }
        
        [Test]
        public void NewCard_AceOfSpades_CorrectValues()
        {
			var blackJackCard = new BlackJackCard(Card, FaceDown);
			Assert.AreEqual(CardRank.Ace, blackJackCard.Rank);
			Assert.AreEqual(CardSuit.Spades, blackJackCard.Suit);
			Assert.AreEqual(FaceDown, blackJackCard.FaceDown);
		}
    }
}
