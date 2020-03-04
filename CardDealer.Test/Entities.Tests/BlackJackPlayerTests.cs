using CardDealer.Tests.Providers.Mocks;
using Entities.Enums;
using Entities.Interfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;
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
		private readonly IHandIdentifierProvider HandIdentifierProvider;

		public BlackJackPlayerTests()
		{
			HandIdentifierProvider = new GuidBasedHandIdentifierProviderMock();
		}

		[Test]
		public void NewBlackJackPlayer_NullName_ArgumentNullException()
		{
			var player = new KeyValuePair<string, Player>("8625cf04-b7e2", null);
			Assert.Throws<NullReferenceException>(() => new BlackJackPlayer(player, HandIdentifierProvider));
		}

		[Test]
		public void NewPlayer_NoCards_HandEmpty()
		{
			var sam = new BlackJackPlayer(new KeyValuePair<string, Player>("8625cf04-b7e2", player1), HandIdentifierProvider);
			sam.AddHands(1);
			Assert.AreEqual(false, sam.Hands.First().Value.Cards.Any());
			Assert.AreEqual(playerName, sam.Name);
			Assert.AreEqual(PlayerStatusTypes.Waiting, sam.Status);
		}

		[Test]
		public void NewPlayer_NoCards_StatusInProgress()
		{
			var sam = new BlackJackPlayer(new KeyValuePair<string, Player>("8625cf04-b7e2", player1), HandIdentifierProvider);
			Assert.AreEqual(PlayerStatusTypes.Waiting, sam.Status);
		}

		[Test]
		public void NewPlayer_CalculateTotal_TotalFive()
		{
			var sam = new BlackJackPlayer(new KeyValuePair<string, Player>("8625cf04-b7e2", player1), HandIdentifierProvider);
			sam.AddHands(1);
			var blkJkTwoClubs = new BlackJackCard(twoClubs, true);
			var blkJkThreeClubs = new BlackJackCard(threeClubs, true);
			sam.Hands.First().Value.AddCard(blkJkTwoClubs);
			sam.Hands.First().Value.AddCard(blkJkThreeClubs);
			Assert.AreEqual(5, sam.Hands.First().Value.PointValue);
		}

		[Test]
		public void Player_DrawCard_TotalFifteen()
		{
			var playerOne = new BlackJackPlayer(new KeyValuePair<string, Player>("8625cf04-b7e2", player1), HandIdentifierProvider);
			playerOne.AddHands(1);
			var blkJkTwoClubs = new BlackJackCard(twoClubs, true);
			var blkJkThreeClubs = new BlackJackCard(threeClubs, true);
			var blkJkJackClubs = new BlackJackCard(jackClubs, true);
			playerOne.Hands.First().Value.AddCard(blkJkTwoClubs);
			playerOne.Hands.First().Value.AddCard(blkJkThreeClubs);
			playerOne.Hands.First().Value.AddCard(blkJkJackClubs);

			Assert.AreEqual(15, playerOne.Hands.First().Value.PointValue);
		}

		[Test]
		public void Player_DrawCard_FirstCardFaceDown()
		{
			var playerOne = new BlackJackPlayer(new KeyValuePair<string, Player>("8625cf04-b7e2", player1), HandIdentifierProvider);
			playerOne.AddHands(1);
			var blkJkTwoClubs = new BlackJackCard(twoClubs, true);
			playerOne.Hands.First().Value.AddCard(blkJkTwoClubs);

			Assert.AreEqual(true, playerOne.Hands.First().Value.Cards.First().FaceDown);
		}

		[Test]
		public void Player_DrawCard_SecondCardFaceUp()
		{
			var playerOne = new BlackJackPlayer(new KeyValuePair<string, Player>("8625cf04-b7e2", player1), HandIdentifierProvider);
			playerOne.AddHands(1);
			var blkJkTwoClubs = new BlackJackCard(twoClubs, true);
			var blkJkThreeClubs = new BlackJackCard(threeClubs, false);
			playerOne.Hands.First().Value.AddCard(blkJkTwoClubs);
			playerOne.Hands.First().Value.AddCard(blkJkThreeClubs);

			Assert.AreEqual(false, playerOne.Hands.First().Value.Cards.ElementAt(1).FaceDown);
		}

		[Test]
		public void Player_DrawCard_ThirdCardFaceUp()
		{
			var playerOne = new BlackJackPlayer(new KeyValuePair<string, Player>("8625cf04-b7e2", player1), HandIdentifierProvider);
			playerOne.AddHands(1);
			var blkJkTwoClubs = new BlackJackCard(twoClubs, true);
			var blkJkThreeClubs = new BlackJackCard(threeClubs, false);
			var blkJkJackClubs = new BlackJackCard(jackClubs, true);
			playerOne.Hands.First().Value.AddCard(blkJkTwoClubs);
			playerOne.Hands.First().Value.AddCard(blkJkThreeClubs);
			playerOne.Hands.First().Value.AddCard(blkJkJackClubs);

			Assert.AreEqual(true, playerOne.Hands.First().Value.Cards.ElementAt(0).FaceDown);
		}
	}
}
