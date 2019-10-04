using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities.Tests
{
	class HandTests
	{
		private BlackJackCard AceSpadesUp = new BlackJackCard(new Card(CardSuit.Spades, CardRank.Ace), false);

		[Test]
		public void NewHand_DefaultValues_CorrectValues()
		{
			var hand = new Hand();
			
			Assert.AreEqual(0, hand.PointValue);
			Assert.AreEqual(false, hand.Cards.Any());
		}

		[Test]
		public void NewHand_AddAceSpadesUp_CorrectValues()
		{
			var hand = new Hand();
			hand.AddCard(AceSpadesUp);

			Assert.AreEqual(, hand.PointValue);
			Assert.AreEqual(false, hand.Cards.Any());
		}
	}
}
