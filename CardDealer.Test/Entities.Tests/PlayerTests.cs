using Entities.Enums;
using NUnit.Framework;
using System;

namespace Entities.Tests
{
	public class PlayerTests
	{
		private readonly Card twoClubs = new Card(CardSuit.Clubs, CardRank.Two);
		private readonly Card threeClubs = new Card(CardSuit.Clubs, CardRank.Three);
		private readonly Card jackClubs = new Card(CardSuit.Clubs, CardRank.Jack);
		private const string playerName = "Sam";

		[Test]
		public void NewPlayer_NullName_ArgumentNullException()
		{
			Assert.Throws<ArgumentNullException>(() => new Player(null));
		}

		[Test]
		public void NewPlayer_Sam_CorrectName()
		{
			var sam = new Player(playerName);
			Assert.AreEqual(playerName, sam.Name);
		}
	}
}
