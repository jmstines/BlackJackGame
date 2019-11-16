using NUnit.Framework;
using System;
using System.Linq;

namespace Entities.Tests
{
	public class BlackJackPlayerTests
	{
		private readonly Card twoClubs = new Card(CardSuit.Clubs, CardRank.Two);
		private readonly Card threeClubs = new Card(CardSuit.Clubs, CardRank.Three);
		private readonly Card jackClubs = new Card(CardSuit.Clubs, CardRank.Jack);
		private const string playerName = "Sam";
		private readonly Player player1 = new Player(playerName);

		[Test]
		public void NewBlackJackPlayer_NullName_ArgumentNullException()
		{
			Assert.Throws<ArgumentNullException>(() => new BlackJackPlayer("8625cf04-b7e2", null));
		}

		[Test]
		public void NewPlayer_NoCards_HandEmpty()
		{
			var sam = new BlackJackPlayer("8625cf04-b7e2", player1);
			Assert.AreEqual(false, sam.Hand.Cards.Any());
			Assert.AreEqual(playerName, sam.Name);
			Assert.AreEqual(PlayerStatusTypes.InProgress, sam.Status);
		}

		[Test]
		public void NewPlayer_NoCards_StatusInProgress()
		{
			var sam = new BlackJackPlayer("8625cf04-b7e2", player1);
			Assert.AreEqual(PlayerStatusTypes.InProgress, sam.Status);
		}

		[Test]
		public void NewPlayer_CalculateTotal_TotalFive()
		{
			var sam = new BlackJackPlayer("8625cf04-b7e2", player1);
			var blkJkTwoClubs = new BlackJackCard(twoClubs, true);
			var blkJkThreeClubs = new BlackJackCard(threeClubs, true);
			sam.AddCardToHand(blkJkTwoClubs);
			sam.AddCardToHand(blkJkThreeClubs);
			Assert.AreEqual(5, sam.Hand.PointValue);
		}

		[Test]
		public void Player_DrawCard_TotalFifteen()
		{
			var playerOne = new BlackJackPlayer("8625cf04-b7e2", player1);
			var blkJkTwoClubs = new BlackJackCard(twoClubs, true);
			var blkJkThreeClubs = new BlackJackCard(threeClubs, true);
			var blkJkJackClubs = new BlackJackCard(jackClubs, true);
			playerOne.AddCardToHand(blkJkTwoClubs);
			playerOne.AddCardToHand(blkJkThreeClubs);
			playerOne.AddCardToHand(blkJkJackClubs);

			Assert.AreEqual(15, playerOne.Hand.PointValue);
		}

		[Test]
		public void Player_DrawCard_FirstCardFaceDown()
		{
			var playerOne = new BlackJackPlayer("8625cf04-b7e2", player1);
			var blkJkTwoClubs = new BlackJackCard(twoClubs, true);
			playerOne.AddCardToHand(blkJkTwoClubs);

			Assert.AreEqual(true, playerOne.Hand.Cards.First().FaceDown);
		}

		[Test]
		public void Player_DrawCard_SecondCardFaceUp()
		{
			var playerOne = new BlackJackPlayer("8625cf04-b7e2", player1);
			var blkJkTwoClubs = new BlackJackCard(twoClubs, true);
			var blkJkThreeClubs = new BlackJackCard(threeClubs, false);
			playerOne.AddCardToHand(blkJkTwoClubs);
			playerOne.AddCardToHand(blkJkThreeClubs);

			Assert.AreEqual(false, playerOne.Hand.Cards.ElementAt(1).FaceDown);
		}

		[Test]
		public void Player_DrawCard_ThirdCardFaceUp()
		{
			var playerOne = new BlackJackPlayer("8625cf04-b7e2", player1);
			var blkJkTwoClubs = new BlackJackCard(twoClubs, true);
			var blkJkThreeClubs = new BlackJackCard(threeClubs, false);
			var blkJkJackClubs = new BlackJackCard(jackClubs, true);
			playerOne.AddCardToHand(blkJkTwoClubs);
			playerOne.AddCardToHand(blkJkThreeClubs);
			playerOne.AddCardToHand(blkJkJackClubs);

			Assert.AreEqual(true, playerOne.Hand.Cards.ElementAt(0).FaceDown);
		}
	}
}
