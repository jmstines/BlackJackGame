using Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Tests
{
    public class BlackJackCardTests
    {
        private readonly bool FaceDown = false;
        private readonly int AceHighValue = 11;
        private readonly int cardInvalidValue = -1;
        private readonly Card Card = new Card(CardSuit.Spades, "A", "Ace");

        [Test]
        public void NewCard_CardNull_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new BlackJackCard(null, FaceDown, AceHighValue));
        }
        
        [Test]
        public void NewCard_EmptyDisplay_ArgumentException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new BlackJackCard(Card, FaceDown, cardInvalidValue));
        }
    }
}
