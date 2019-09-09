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
            Assert.Throws<ArgumentNullException>(() => new Card(Suit.Clubs, null, AceDescription));
        }

        [Test]
        public void NewCard_NullDescription_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new Card(Suit.Clubs, AceDisplay, null));
        }
    }
}
