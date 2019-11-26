using Entities.Enums;
using NUnit.Framework;
using System;

namespace Entities.Tests
{
	public class BlackJackCardTests
	{
		private readonly bool FaceDown = false;
		private readonly Card AceOfSpades = new Card(CardSuit.Spades, CardRank.Ace);
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
			var aceOfSpades = new BlackJackCard(AceOfSpades, true);
			var aceOfSpades2 = new BlackJackCard(AceOfSpades, false);
			Assert.IsTrue(aceOfSpades.Equals(aceOfSpades2));
		}

		[Test]
		public void NewCard_DifferentRank_NotEqual()
		{
			var aceOfSpades = new BlackJackCard(AceOfSpades, true);
			var tenOfSpades = new BlackJackCard(TenOfSpades, false);
			Assert.IsFalse(aceOfSpades.Equals(tenOfSpades));
		}

		[Test]
		public void NewCard_DifferentSuit_NotEqual()
		{
			var aceOfSpades = new BlackJackCard(AceOfSpades, true);
			var aceOfClubs = new BlackJackCard(AceOfClubs, false);
			Assert.IsFalse(aceOfSpades.Equals(aceOfClubs));
		}
	}
}
