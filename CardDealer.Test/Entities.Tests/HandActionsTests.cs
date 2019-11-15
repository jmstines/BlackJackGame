using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Entities.Tests
{
	class HandActionsTests
	{
		[Test]
		public void NewHand_DefaultValues_CorrectValues()
		{
			var deck = new Deck();
			var hand = new List<Card>(0);

			//Assert.AreEqual(0, hand.PointValue);
			//Assert.AreEqual(false, hand.Cards.Any());
		}
	}
}
