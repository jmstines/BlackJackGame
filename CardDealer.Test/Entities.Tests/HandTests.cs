using Entities.Enums;
using Entities.Interfaces;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Entities.Tests
{
	class HandTests
	{
		private readonly IEnumerable<HandActionTypes> DefualtActions = new List<HandActionTypes> { HandActionTypes.Draw, HandActionTypes.Hold };
		private readonly IEnumerable<HandActionTypes> SplitHandActions = new List<HandActionTypes> { HandActionTypes.Draw, HandActionTypes.Hold, HandActionTypes.Split };
		private readonly IEnumerable<HandActionTypes> EndHandActions = new List<HandActionTypes> { HandActionTypes.Pass };
		private readonly Deck DefaultDeck = new Deck();

		[Test]
		public void NewHand_DefaultValues_CorrectValues()
		{
			var hand = new Hand("1234-QWERY");

			Assert.AreEqual(0, hand.PointValue);
			Assert.AreEqual(false, hand.Cards.Any());
			Assert.AreEqual(HandStatusTypes.InProgress, hand.Status);
			Assert.AreEqual(DefualtActions, hand.Actions);
		}

		[Test]
		public void NewHand_AddAceSpadesUp_CorrectValues()
		{
			var hand = new Hand("4567-ASDF");
			hand.AddCard(DefaultDeck.First(c => c.Rank.Equals(CardRank.Ace)));

			Assert.AreEqual(11, hand.PointValue);
			Assert.AreEqual(true, hand.Cards.First().FaceDown);
			Assert.AreEqual(HandStatusTypes.InProgress, hand.Status);
			Assert.AreEqual(DefualtActions, hand.Actions);
		}

		[Test]
		public void AfterDeal_AddAceSpadesUp_CorrectValues()
		{
			var hand = new Hand("7890-ZXCVB");
			hand.AddCard(DefaultDeck.First(c => c.Rank.Equals(CardRank.Jack)));
			hand.AddCard(DefaultDeck.First(c => c.Rank.Equals(CardRank.Ten)));
			hand.AddCard(DefaultDeck.First(c => c.Rank.Equals(CardRank.Ace)));

			Assert.AreEqual(21, hand.PointValue);
			Assert.AreEqual(true, hand.Cards.First().FaceDown);
			Assert.AreEqual(HandStatusTypes.InProgress, hand.Status);
			Assert.AreEqual(EndHandActions, hand.Actions);
		}

		[Test]
		public void HandDeal_DefaultValues_CorrectValues()
		{
			var hand = new Hand("1234-YUIOP");
			hand.AddCard(DefaultDeck.First(c => c.Rank.Equals(CardRank.Ace)));
			hand.AddCard(DefaultDeck.First(c => c.Rank.Equals(CardRank.Jack)));

			Assert.AreEqual(EndHandActions, hand.Actions);
			Assert.AreEqual(21, hand.PointValue);
			Assert.AreEqual(2, hand.Cards.Count());
			Assert.AreEqual(HandStatusTypes.InProgress, hand.Status);
			Assert.AreEqual(EndHandActions, hand.Actions);
		}

		[Test]
		public void NewHand_DefaultValues_IncorrectValues()
		{
			var hand = new Hand("3210-JHJKL");
			hand.AddCard(DefaultDeck.First(c => c.Rank.Equals(CardRank.Three)));
			hand.AddCard(DefaultDeck.First(c => c.Rank.Equals(CardRank.Jack)));

			Assert.AreNotEqual(EndHandActions, hand.Actions);
			Assert.AreEqual(13, hand.PointValue);
			Assert.AreEqual(2, hand.Cards.Count());
			Assert.AreEqual(HandStatusTypes.InProgress, hand.Status);
		}

		[Test]
		public void NewHand2TenValues_SplitAndDefaultHand_CorrectValues()
		{
			var hand = new Hand("9874-BNMV");
			hand.AddCard(DefaultDeck.First(c => c.Rank.Equals(CardRank.King)));
			hand.AddCard(DefaultDeck.First(c => c.Rank.Equals(CardRank.Jack)));

			Assert.AreEqual(20, hand.PointValue);
			Assert.AreEqual(2, hand.Cards.Count());
			Assert.AreNotEqual(SplitHandActions, hand.Actions);
			Assert.AreEqual(HandStatusTypes.InProgress, hand.Status);
		}

		[Test]
		public void BustHand_EndHandValues_CorrectValues()
		{
			var hand = new Hand("3210-POIUY");
			hand.AddCard(DefaultDeck.First(c => c.Rank.Equals(CardRank.King)));
			hand.AddCard(DefaultDeck.First(c => c.Rank.Equals(CardRank.Jack)));
			hand.AddCard(DefaultDeck.First(c => c.Rank.Equals(CardRank.Queen)));

			Assert.AreEqual(30, hand.PointValue);
			Assert.AreEqual(3, hand.Cards.Count());
			Assert.AreEqual(EndHandActions, hand.Actions);
			Assert.AreEqual(HandStatusTypes.Bust, hand.Status);
		}

		[Test]
		public void AceAndTen_GetValue_ValueEqualsBlackJack()
		{
			var hand = new Hand("6547-IUYTR");
			hand.AddCard(DefaultDeck.First(c => c.Rank.Equals(CardRank.Ace)));
			hand.AddCard(DefaultDeck.First(c => c.Rank.Equals(CardRank.Ten)));

			Assert.AreEqual(BlackJackConstants.BlackJack, hand.PointValue);
		}

		[Test]
		public void AceTenTen_GetValue_ValueEqualsBlackJack()
		{
			var hand = new Hand("7410-HGFDS");
			hand.AddCard(DefaultDeck.First(c => c.Rank.Equals(CardRank.Ace)));
			hand.AddCard(DefaultDeck.First(c => c.Rank.Equals(CardRank.Ten)));
			hand.AddCard(DefaultDeck.First(c => c.Rank.Equals(CardRank.Ten)));

			Assert.AreEqual(BlackJackConstants.BlackJack, hand.PointValue);
		}

		[Test]
		public void AceAceTen_GetValue_ValueEqualsTwelve()
		{
			var hand = new Hand("9632-YUITR");
			hand.AddCard(DefaultDeck.First(c => c.Rank.Equals(CardRank.Ace)));
			hand.AddCard(DefaultDeck.First(c => c.Rank.Equals(CardRank.Ace)));
			hand.AddCard(DefaultDeck.First(c => c.Rank.Equals(CardRank.Ten)));

			Assert.AreEqual(12, hand.PointValue);
		}
	}
}
