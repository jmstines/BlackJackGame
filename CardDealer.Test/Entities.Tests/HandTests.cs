using Entities.Interfaces;
using NUnit.Framework;
using System.Linq;

namespace Entities.Tests
{
	class HandTests
	{
		private readonly ICard AceSpadesUp = new Card(CardSuit.Spades, CardRank.Ace);
		private readonly ICard TenSpadesDown = new Card(CardSuit.Spades, CardRank.Ten);
		private readonly ICard TenSpadesUp = new Card(CardSuit.Spades, CardRank.Ten);

		[Test]
		public void NewHand_DefaultValues_CorrectValues()
		{
			var hand = new Hand();
			
			Assert.AreEqual(0, hand.PointValue);
			Assert.AreEqual(false, hand.Cards.Any());
			Assert.AreEqual(HandStatusTypes.InProgress, hand.Status);
		}

		[Test]
		public void NewHand_AddAceSpadesUp_CorrectValues()
		{
			var hand = new Hand();
			hand.AddCard(AceSpadesUp);

			Assert.AreEqual(11, hand.PointValue);
			Assert.AreEqual(true, hand.Cards.First().FaceDown);
			Assert.AreEqual(HandStatusTypes.InProgress, hand.Status);
		}

		[Test]
		public void AfterDeal_AddAceSpadesUp_CorrectValues()
		{
			var hand = new Hand();
			hand.AddCard(TenSpadesDown);
			hand.AddCard(TenSpadesUp);
			hand.AddCard(AceSpadesUp);

			Assert.AreEqual(21, hand.PointValue);
			Assert.AreEqual(true, hand.Cards.First().FaceDown);
			Assert.AreEqual(HandStatusTypes.InProgress, hand.Status);
		}
	}
}
