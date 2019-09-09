using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Tests
{
    public class CardDetailTests
    {
        private readonly string AceDisplay = "A";
        private readonly string AceDescription = "Ace";

        [Test]
        public void NewCardDetail_DescriptionNull_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new CardDetail(AceDisplay, null));
        }

        [Test]
        public void NewCardDetail_DisplayNull_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new CardDetail(null, AceDescription));
        }

        [Test]
        public void NewCardDetail_DisplayEmpty_ArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new CardDetail("", AceDescription));
        }

        [Test]
        public void NewCardDetail_AceCard_CorrectValues()
        {
            var card = new CardDetail(AceDisplay, AceDescription);
            Assert.AreEqual(AceDisplay, card.Display);
            Assert.AreEqual(AceDescription, card.Description);
        }
    }
}
