using NUnit.Framework;
using System;

namespace Entities.Tests
{
    public class CardTests
    {
        private readonly string AceDisplay = "A";
        private readonly string AceDescription = "Ace";

        [Test]
        public void NewCard_NullDisplayValue_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new Card(CardSuit.Clubs, null, AceDescription));
        }

        [Test]
        public void NewCard_NullDescription_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new Card(CardSuit.Clubs, AceDisplay, null));
        }

        [Test]
        public void NewCard_EmptyDisplay_ArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new Card(CardSuit.Clubs, string.Empty, AceDescription));
        }

        [Test]
        public void NewCard_WhiteSpaceDisplay_ArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new Card(CardSuit.Clubs, "   ", AceDescription));
        }
    }
}
