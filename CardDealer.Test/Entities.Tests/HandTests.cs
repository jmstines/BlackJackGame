using Entities.Enums;
using Entities.Interfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Entities.Tests
{
	class HandTests
	{
		//private readonly IEnumerable<HandActionTypes> SplitHandActions = new List<HandActionTypes> { HandActionTypes.Draw, HandActionTypes.Hold, HandActionTypes.Split };
		private readonly Deck DefaultDeck = new Deck();

		[Test]
		public void NewHand_DefaultValues_CorrectValues()
		{
			var hand = new Hand("1234-QWERY");

			Assert.AreEqual(0, hand.PointValue);
			Assert.AreEqual(false, hand.Cards.Any());
			Assert.AreEqual(HandStatusTypes.InProgress, hand.Status);
			Assert.True(hand.Actions.Contains(HandActionTypes.Draw));
			Assert.True(hand.Actions.Contains(HandActionTypes.Hold));
			Assert.False(hand.Actions.Contains(HandActionTypes.Split));
			Assert.False(hand.Actions.Contains(HandActionTypes.Pass));
		}

		[Test]
		public void NewHand_AddAceSpadesUp_CorrectValues()
		{
			var hand = new Hand("4567-ASDF");
			hand.AddCard(DefaultDeck.First(c => c.Rank.Equals(CardRank.Ace)));

			Assert.AreEqual(11, hand.PointValue);
			Assert.AreEqual(true, hand.Cards.First().FaceDown);
			Assert.AreEqual(HandStatusTypes.InProgress, hand.Status);
			Assert.True(hand.Actions.Contains(HandActionTypes.Draw));
			Assert.True(hand.Actions.Contains(HandActionTypes.Hold));
			Assert.False(hand.Actions.Contains(HandActionTypes.Split));
			Assert.False(hand.Actions.Contains(HandActionTypes.Pass));
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
			Assert.Contains(HandActionTypes.Pass, hand.Actions.ToList());
		}

		[Test]
		public void HandDeal_DefaultValues_CorrectValues()
		{
			var hand = new Hand("1234-YUIOP");
			hand.AddCard(DefaultDeck.First(c => c.Rank.Equals(CardRank.Ace)));
			hand.AddCard(DefaultDeck.First(c => c.Rank.Equals(CardRank.Jack)));

			Assert.AreEqual(21, hand.PointValue);
			Assert.AreEqual(2, hand.Cards.Count());
			Assert.AreEqual(HandStatusTypes.InProgress, hand.Status);
			Assert.True(hand.Actions.Contains(HandActionTypes.Pass));
		}

		[Test]
		public void NewHand_DefaultValues_IncorrectValues()
		{
			var hand = new Hand("3210-JHJKL");
			hand.AddCard(DefaultDeck.First(c => c.Rank.Equals(CardRank.Three)));
			hand.AddCard(DefaultDeck.First(c => c.Rank.Equals(CardRank.Jack)));

			Assert.False(hand.Actions.Contains(HandActionTypes.Pass));
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
			Assert.True(hand.Actions.Contains(HandActionTypes.Draw));
			Assert.True(hand.Actions.Contains(HandActionTypes.Hold));
			Assert.True(hand.Actions.Contains(HandActionTypes.Split));
			Assert.False(hand.Actions.Contains(HandActionTypes.Pass));
			Assert.AreEqual(HandStatusTypes.InProgress, hand.Status);
		}

		[Test]
		public void NewHand2TenValues_SplitHand_CardsCountZero()
		{
			var hand = new Hand("9874-BNMV");
			hand.AddCard(DefaultDeck.First(c => c.Rank.Equals(CardRank.King)));
			hand.AddCard(DefaultDeck.First(c => c.Rank.Equals(CardRank.Jack)));

			Assert.AreEqual(20, hand.PointValue);
			Assert.AreEqual(2, hand.Cards.Count());
			Assert.True(hand.Actions.Contains(HandActionTypes.Draw));
			Assert.True(hand.Actions.Contains(HandActionTypes.Hold));
			Assert.True(hand.Actions.Contains(HandActionTypes.Split));
			Assert.False(hand.Actions.Contains(HandActionTypes.Pass));
			Assert.AreEqual(HandStatusTypes.InProgress, hand.Status);

			hand.Split();

			Assert.AreEqual(0, hand.PointValue);
			Assert.AreEqual(0, hand.Cards.Count());
			Assert.False(hand.Actions.Contains(HandActionTypes.Draw));
			Assert.False(hand.Actions.Contains(HandActionTypes.Hold));
			Assert.False(hand.Actions.Contains(HandActionTypes.Split));
			Assert.True(hand.Actions.Contains(HandActionTypes.Pass));
			Assert.AreEqual(HandStatusTypes.Hold, hand.Status);
		}

		[Test]
		public void NewHand2TenValues_TrySplitHand_ThrowInvalidOperationException()
		{
			var hand = new Hand("9874-BNMV");
			hand.AddCard(DefaultDeck.First(c => c.Rank.Equals(CardRank.King)));
			hand.AddCard(DefaultDeck.First(c => c.Rank.Equals(CardRank.Jack)));

			Assert.AreEqual(20, hand.PointValue);
			Assert.AreEqual(2, hand.Cards.Count());
			Assert.True(hand.Actions.Contains(HandActionTypes.Draw));
			Assert.True(hand.Actions.Contains(HandActionTypes.Hold));
			Assert.True(hand.Actions.Contains(HandActionTypes.Split));
			Assert.False(hand.Actions.Contains(HandActionTypes.Pass));
			Assert.AreEqual(HandStatusTypes.InProgress, hand.Status);

			hand.Hold();
			Assert.Throws<InvalidOperationException>(() => hand.Split());

			Assert.AreEqual(20, hand.PointValue);
			Assert.AreEqual(2, hand.Cards.Count());
			Assert.False(hand.Actions.Contains(HandActionTypes.Draw));
			Assert.False(hand.Actions.Contains(HandActionTypes.Hold));
			Assert.False(hand.Actions.Contains(HandActionTypes.Split));
			Assert.True(hand.Actions.Contains(HandActionTypes.Pass));
			Assert.AreEqual(HandStatusTypes.Hold, hand.Status);
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
			Assert.Contains(HandActionTypes.Pass, hand.Actions.ToList());
			Assert.AreEqual(HandStatusTypes.Bust, hand.Status);
		}

		[Test]
		public void HandPoint20_SetStatusHold_SetStatusHoldCorrectlyNoOtherChanges()
		{
			var hand = new Hand("3210-POIUY");
			hand.AddCard(DefaultDeck.First(c => c.Rank.Equals(CardRank.King)));
			hand.AddCard(DefaultDeck.First(c => c.Rank.Equals(CardRank.Jack)));

			Assert.AreEqual(20, hand.PointValue);
			Assert.AreEqual(2, hand.Cards.Count());
			Assert.Contains(HandActionTypes.Draw, hand.Actions.ToList());
			Assert.Contains(HandActionTypes.Hold, hand.Actions.ToList());
			Assert.Contains(HandActionTypes.Split, hand.Actions.ToList());
			Assert.AreEqual(HandStatusTypes.InProgress, hand.Status);

			hand.Hold();

			Assert.AreEqual(20, hand.PointValue);
			Assert.AreEqual(2, hand.Cards.Count());
			Assert.False(hand.Actions.Contains(HandActionTypes.Draw));
			Assert.False(hand.Actions.Contains(HandActionTypes.Hold));
			Assert.False(hand.Actions.Contains(HandActionTypes.Split));
			Assert.True(hand.Actions.Contains(HandActionTypes.Pass));
		}

		[Test]
		public void BustHand_TrySetStatus_ThrowsInvalidOperation()
		{
			var hand = new Hand("3210-POIUY");
			hand.AddCard(DefaultDeck.First(c => c.Rank.Equals(CardRank.King)));
			hand.AddCard(DefaultDeck.First(c => c.Rank.Equals(CardRank.Jack)));
			hand.AddCard(DefaultDeck.First(c => c.Rank.Equals(CardRank.Queen)));

			Assert.AreEqual(30, hand.PointValue);
			Assert.AreEqual(3, hand.Cards.Count());
			Assert.Contains(HandActionTypes.Pass, hand.Actions.ToList());
			Assert.AreEqual(HandStatusTypes.Bust, hand.Status);

			Assert.Throws<InvalidOperationException>(() => hand.Hold());
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
			hand.AddCard(DefaultDeck.First(c => c.Rank.Equals(CardRank.Ten)));
			hand.AddCard(DefaultDeck.First(c => c.Rank.Equals(CardRank.Ten)));
			hand.AddCard(DefaultDeck.First(c => c.Rank.Equals(CardRank.Ace)));

			Assert.AreEqual(BlackJackConstants.BlackJack, hand.PointValue);
		}

		[Test]
		public void TenTenTenTen_GetValue_ThrowsInvalidOperation()
		{
			var hand = new Hand("7410-HGFDS");
			hand.AddCard(DefaultDeck.First(c => c.Rank.Equals(CardRank.Ten)));
			hand.AddCard(DefaultDeck.First(c => c.Rank.Equals(CardRank.Ten)));
			hand.AddCard(DefaultDeck.First(c => c.Rank.Equals(CardRank.Ten)));

			Assert.Throws<InvalidOperationException>(() => hand.AddCard(DefaultDeck.First(c => c.Rank.Equals(CardRank.Ten))));
			Assert.AreEqual(30, hand.PointValue);
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

		[Test]
		public void NewHand_AddCardRangeAddNull_ThrowsArgumentNullException()
		{
			var hand = new Hand("9632-YUITR");
			Assert.Throws<ArgumentNullException>(() => hand.AddCard(null));
		}

		[Test]
		public void NewHand_AddCardRangeAddListofNull_ThrowsArgumentNullException()
		{
			var hand = new Hand("9632-YUITR");
			var cards = new List<ICard>() { null, null };
			Assert.Throws<ArgumentNullException>(() => hand.AddCardRange(null));
		}
	}
}
