using Entities.Enums;
using NUnit.Framework;

namespace Entities.Tests
{
	public class CardTests
	{
		[Test]
		public void NewCard_NullDisplayValue_ArgumentNullException()
		{
			var card = new Card(CardSuit.Clubs, CardRank.Ace);
			Assert.AreEqual(CardSuit.Clubs, card.Suit);
			Assert.AreEqual(CardRank.Ace, card.Rank);
		}

		[Test]
		public void NewCards_DuplicateCards_AreEqual()
		{
			var aceOfClubs = new Card(CardSuit.Clubs, CardRank.Ace);
			var aceOfClubs2 = new Card(CardSuit.Clubs, CardRank.Ace);
			Assert.IsTrue(aceOfClubs.Equals(aceOfClubs2));
		}

		[Test]
		public void NewCards_DifferentSuit_NotEqual()
		{
			var aceOfClubs = new Card(CardSuit.Diamonds, CardRank.Ace);
			var aceOfClubs2 = new Card(CardSuit.Clubs, CardRank.Ace);
			Assert.IsFalse(aceOfClubs.Equals(aceOfClubs2));
		}

		[Test]
		public void NewCards_DifferentRank_NotEqual()
		{
			var aceOfClubs = new Card(CardSuit.Clubs, CardRank.Ten);
			var aceOfClubs2 = new Card(CardSuit.Clubs, CardRank.Ace);
			Assert.IsFalse(aceOfClubs.Equals(aceOfClubs2));
		}
	}
}
