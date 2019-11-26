using Entities.Enums;
using NUnit.Framework;
using System;

namespace Entities.Tests
{
	public class BlackJackCardTests
	{
		private readonly bool FaceDown = false;
		private readonly Card AceOfSpades = new Card(CardSuit.Spades, CardRank.Ace);
		private readonly BlackJackCard BlackJackAceOfSpades = new BlackJackCard(new Card(CardSuit.Spades, CardRank.Ace), true);
		private readonly Card AceOfClubs = new Card(CardSuit.Clubs, CardRank.Ace);
		private readonly Card TenOfSpades = new Card(CardSuit.Spades, CardRank.Ten);

		[Test]
		public void NewCard_CardNull_ArgumentNullException()
		{
			Assert.Throws<ArgumentOutOfRangeException>(() => new BlackJackCard(new Card(), FaceDown));
		}

		[Test]
		public void NewCard_AceOfSpades_CorrectValues()
		{
			var blackJackCard = new BlackJackCard(AceOfSpades, FaceDown);
			Assert.AreEqual(CardRank.Ace, blackJackCard.Rank);
			Assert.AreEqual(CardSuit.Spades, blackJackCard.Suit);
			Assert.AreEqual(FaceDown, blackJackCard.FaceDown);
		}

		[Test]
		public void NewCard_AceOfSpades_EqualToAceOfSpadesRegardlessOfFace()
		{
			var aceOfSpades2 = new BlackJackCard(AceOfSpades, false);
			Assert.IsTrue(BlackJackAceOfSpades.Equals(aceOfSpades2));
		}

		[Test]
		public void NewCard_DifferentRank_NotEqual()
		{
			var tenOfSpades = new BlackJackCard(TenOfSpades, false);
			Assert.IsFalse(BlackJackAceOfSpades.Equals(tenOfSpades));
		}

		[Test]
		public void NewCard_DifferentSuit_NotEqual()
		{
			var aceOfClubs = new BlackJackCard(AceOfClubs, false);
			Assert.IsFalse(BlackJackAceOfSpades.Equals(aceOfClubs));
		}

		[Test]
		public void Ace_GetValue_ValueEquals_11()
		{

			Assert.AreEqual(11, BlackJackAceOfSpades.Value);
		}

		[Test]
		public void Two_GetValue_ValueEquals_2()
		{
			var twoOfSpades = new BlackJackCard(new Card(CardSuit.Spades, CardRank.Two), true);
			Assert.AreEqual(2, twoOfSpades.Value);
		}

		[Test]
		public void Three_GetValue_ValueEquals_3()
		{
			var threeOfSpades = new BlackJackCard(new Card(CardSuit.Spades, CardRank.Three), true);
			Assert.AreEqual(3, threeOfSpades.Value);
		}

		[Test]
		public void Four_GetValue_ValueEquals_4()
		{
			var fourOfSpades = new BlackJackCard(new Card(CardSuit.Spades, CardRank.Four), true);
			Assert.AreEqual(4, fourOfSpades.Value);
		}

		[Test]
		public void Five_GetValue_ValueEquals_5()
		{
			var fiveOfSpades = new BlackJackCard(new Card(CardSuit.Spades, CardRank.Five), true);
			Assert.AreEqual(5, fiveOfSpades.Value);
		}

		[Test]
		public void Six_GetValue_ValueEquals_6()
		{
			var sixOfSpades = new BlackJackCard(new Card(CardSuit.Spades, CardRank.Six), true);
			Assert.AreEqual(6, sixOfSpades.Value);
		}

		[Test]
		public void Seven_GetValue_ValueEquals_7()
		{
			var sevenOfSpades = new BlackJackCard(new Card(CardSuit.Spades, CardRank.Seven), true);
			Assert.AreEqual(7, sevenOfSpades.Value);
		}

		[Test]
		public void Eight_GetValue_ValueEquals_8()
		{
			var eightOfSpades = new BlackJackCard(new Card(CardSuit.Spades, CardRank.Eight), true);
			Assert.AreEqual(8, eightOfSpades.Value);
		}

		[Test]
		public void Nine_GetValue_ValueEquals_9()
		{
			var nineOfSpades = new BlackJackCard(new Card(CardSuit.Spades, CardRank.Nine), true);
			Assert.AreEqual(9, nineOfSpades.Value);
		}

		[Test]
		public void Ten_GetValue_ValueEquals_10()
		{
			var tenOfSpades = new BlackJackCard(new Card(CardSuit.Spades, CardRank.Ten), true);
			Assert.AreEqual(10, tenOfSpades.Value);
		}

		[Test]
		public void Jack_GetValue_ValueEquals_10()
		{
			var jackOfSpades = new BlackJackCard(new Card(CardSuit.Spades, CardRank.Jack), true);
			Assert.AreEqual(10, jackOfSpades.Value);
		}

		[Test]
		public void Queen_GetValue_ValueEquals_10()
		{
			var queenOfSpades = new BlackJackCard(new Card(CardSuit.Spades, CardRank.Queen), true);
			Assert.AreEqual(10, queenOfSpades.Value);
		}

		[Test]
		public void King_GetValue_ValueEquals_10()
		{
			var kingOfSpades = new BlackJackCard(new Card(CardSuit.Spades, CardRank.King), true);
			Assert.AreEqual(10, kingOfSpades.Value);
		}
	}
}
