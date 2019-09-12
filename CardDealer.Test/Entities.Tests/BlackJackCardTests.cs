using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Tests
{
    public class BlackJackCardTests
    {
        private readonly string AceDisplay = "A";
        private readonly string AceDescription = "Ace";
        private readonly Suit Suit = Suit.Spades;
        private readonly bool FaceDown = false;
        private readonly int value = 11;

        [Test]
        public void NewCard_DisplayNull_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new BlackJackCard(Suit, null, AceDescription, FaceDown));
        }
        
        [Test]
        public void NewCard_EmptyDisplay_ArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new BlackJackCard(Suit, string.Empty, AceDescription, FaceDown));
        }

        [Test]
        public void NewCard_WhiteSpaceDisplay_ArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new BlackJackCard(Suit, "   ", AceDescription, FaceDown));
        }

        [Test]
        public void NewCard_DescriptionNull_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new BlackJackCard(Suit, AceDisplay, null, FaceDown));
        }

        [Test]
        public void NewCard_AceValueEleven_ArgumentNullException()
        {
            BlackJackCard card = new BlackJackCard(Suit, AceDisplay, AceDescription, FaceDown);
            Assert.AreEqual(value, card.Value);
        }
    }
}
