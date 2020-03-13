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
		private readonly ICard twoClubs;
		private readonly Card threeClubs = new Card(CardSuit.Clubs, CardRank.Three);
		private readonly Card jackClubs = new Card(CardSuit.Clubs, CardRank.Jack);
		private readonly IEnumerable<ICard> twoThreeClubs;
		private const string playerName = "Sam";
		private readonly Avitar player1 = new Avitar(playerName);
		private readonly IHandIdentifierProvider HandIdentifierProvider;
		private readonly CardProviderMock cardProvider;

		public BlackJackPlayerTests()
		{
			HandIdentifierProvider = new GuidBasedHandIdentifierProviderMock();
			cardProvider = new CardProviderMock();
			twoClubs = cardProvider.Cards(CardRank.Two, CardSuit.Clubs).Single();
			twoThreeClubs = cardProvider.Cards(new List<CardRank>() { CardRank.Two, CardRank.Three });
		}

		[Test]
		public void NewBlackJackPlayer_NullName_ArgumentNullException()
		{
			var player = new KeyValuePair<string, Avitar>("8625cf04-b7e2", null);
			Assert.Throws<NullReferenceException>(() => new BlackJackPlayer(player, HandIdentifierProvider, 1));
		}

		[Test]
		public void NewPlayer_NoCards_HandEmpty()
		{
			var sam = new BlackJackPlayer(new KeyValuePair<string, Avitar>("8625cf04-b7e2", player1), HandIdentifierProvider, 1);

			Assert.AreEqual(1, sam.Hands.Count());
			Assert.AreEqual(false, sam.Hands.First().Cards.Any());
			Assert.AreEqual(playerName, sam.Name);
			Assert.AreEqual(PlayerStatusTypes.Waiting, sam.Status);
		}

		[Test]
		public void NewPlayer_NoCards_StatusInProgress()
		{
			var sam = new BlackJackPlayer(new KeyValuePair<string, Avitar>("8625cf04-b7e2", player1), HandIdentifierProvider, 1);
			Assert.AreEqual(PlayerStatusTypes.Waiting, sam.Status);
		}

		[Test]
		public void NewPlayer_CalculateTotal_TotalFive()
		{
			var sam = new BlackJackPlayer(new KeyValuePair<string, Avitar>("8625cf04-b7e2", player1), HandIdentifierProvider, 1);

			var handId = new GuidBasedHandIdentifierProviderMock().GenerateHandIds(1).Single();
			sam.DealHands(twoThreeClubs);
			Assert.AreEqual(5, sam.Hands.First().PointValue);
		}

		[Test]
		public void PlayerSingleHand_DealHand_CorrectStartingValues()
		{
			var playerId = "8625cf04-b7e2";
			var playerOne = new BlackJackPlayer(new KeyValuePair<string, Avitar>(playerId, player1), HandIdentifierProvider, 1);

			var handId = new GuidBasedHandIdentifierProviderMock().GenerateHandIds(1).Single();
			playerOne.DealHands(twoThreeClubs);

			Assert.AreEqual(true, playerOne.Hands.Single(h => h.Identifier == handId).Cards.ElementAt(0).FaceDown);
			Assert.AreEqual(false, playerOne.Hands.Single(h => h.Identifier == handId).Cards.ElementAt(1).FaceDown);
			Assert.AreEqual(5, playerOne.Hands.Single(h => h.Identifier == handId).PointValue);
			Assert.AreEqual(PlayerStatusTypes.InProgress, playerOne.Status);
			Assert.AreEqual(playerId, playerOne.Identifier);
			Assert.AreEqual(1, playerOne.Hands.Count());
			Assert.AreEqual(HandStatusTypes.InProgress, playerOne.Hands.Single(h => h.Identifier == handId).Status);
		}

		[Test]
		public void PlayerSingleHand_DealAndHit_CorrectInstanceValues()
		{
			var playerId = "8625cf04-b7e2";
			var playerOne = new BlackJackPlayer(new KeyValuePair<string, Avitar>(playerId, player1), HandIdentifierProvider, 1);
			var blkJkJackClubs = new BlackJackCard(jackClubs, true);
			var handId = new GuidBasedHandIdentifierProviderMock().GenerateHandIds(1).Single();

			playerOne.DealHands(twoThreeClubs);
			Assert.AreEqual(PlayerStatusTypes.InProgress, playerOne.Status);

			playerOne.Hit(handId, blkJkJackClubs);

			Assert.AreEqual(true, playerOne.Hands.Single(h => h.Identifier == handId).Cards.ElementAt(0).FaceDown);
			Assert.AreEqual(false, playerOne.Hands.Single(h => h.Identifier == handId).Cards.ElementAt(1).FaceDown);
			Assert.AreEqual(false, playerOne.Hands.Single(h => h.Identifier == handId).Cards.ElementAt(2).FaceDown);
			Assert.AreEqual(15, playerOne.Hands.Single(h => h.Identifier == handId).PointValue);
			Assert.AreEqual(PlayerStatusTypes.InProgress, playerOne.Status);
			Assert.AreEqual(playerId, playerOne.Identifier);
			Assert.AreEqual(1, playerOne.Hands.Count());
			Assert.AreEqual(HandStatusTypes.InProgress, playerOne.Hands.Single(h => h.Identifier == handId).Status);
		}

		[Test]
		public void PlayerSingleHand_DealAndHitAndHold_CorrectInstanceValues()
		{
			var playerId = "8625cf04-b7e2";
			var playerOne = new BlackJackPlayer(new KeyValuePair<string, Avitar>(playerId, player1), HandIdentifierProvider, 1);
			var blkJkJackClubs = new BlackJackCard(jackClubs, true);
			var handId = new GuidBasedHandIdentifierProviderMock().GenerateHandIds(1).Single();

			playerOne.DealHands(twoThreeClubs);
			Assert.AreEqual(PlayerStatusTypes.InProgress, playerOne.Status);

			playerOne.Hit(handId, blkJkJackClubs);

			Assert.AreEqual(true, playerOne.Hands.Single(h => h.Identifier == handId).Cards.ElementAt(0).FaceDown);
			Assert.AreEqual(false, playerOne.Hands.Single(h => h.Identifier == handId).Cards.ElementAt(1).FaceDown);
			Assert.AreEqual(false, playerOne.Hands.Single(h => h.Identifier == handId).Cards.ElementAt(2).FaceDown);
			Assert.AreEqual(15, playerOne.Hands.Single(h => h.Identifier == handId).PointValue);
			Assert.AreEqual(PlayerStatusTypes.InProgress, playerOne.Status);
			Assert.AreEqual(playerId, playerOne.Identifier);
			Assert.AreEqual(1, playerOne.Hands.Count());
			Assert.AreEqual(HandStatusTypes.InProgress, playerOne.Hands.Single(h => h.Identifier == handId).Status);

			playerOne.Hold(handId);

			Assert.AreEqual(true, playerOne.Hands.Single(h => h.Identifier == handId).Cards.ElementAt(0).FaceDown);
			Assert.AreEqual(false, playerOne.Hands.Single(h => h.Identifier == handId).Cards.ElementAt(1).FaceDown);
			Assert.AreEqual(false, playerOne.Hands.Single(h => h.Identifier == handId).Cards.ElementAt(2).FaceDown);
			Assert.AreEqual(15, playerOne.Hands.Single(h => h.Identifier == handId).PointValue);
			Assert.AreEqual(PlayerStatusTypes.Complete, playerOne.Status);
			Assert.AreEqual(playerId, playerOne.Identifier);
			Assert.AreEqual(1, playerOne.Hands.Count());
			Assert.AreEqual(HandStatusTypes.Hold, playerOne.Hands.Single(h => h.Identifier == handId).Status);
		}

		[Test]
		public void Player2Hands_DealHand_CorrectStartingValues()
		{
			var playerId = "8625cf04-b7e2";
			var playerOne = new BlackJackPlayer(new KeyValuePair<string, Avitar>(playerId, player1), HandIdentifierProvider, 2);

			var handId = new GuidBasedHandIdentifierProviderMock().GenerateHandIds(1).Single();
			playerOne.DealHands(twoThreeClubs);

			Assert.AreEqual(true, playerOne.Hands.Single(h => h.Identifier == handId).Cards.ElementAt(0).FaceDown);
			Assert.AreEqual(false, playerOne.Hands.Single(h => h.Identifier == handId).Cards.ElementAt(1).FaceDown);
			Assert.AreEqual(5, playerOne.Hands.Single(h => h.Identifier == handId).PointValue);
			Assert.AreEqual(PlayerStatusTypes.InProgress, playerOne.Status);
			Assert.AreEqual(playerId, playerOne.Identifier);
			Assert.AreEqual(2, playerOne.Hands.Count());
			Assert.AreEqual(HandStatusTypes.InProgress, playerOne.Hands.Single(h => h.Identifier == handId).Status);
			// TODO Test Second Hand Values
		}

		[Test]
		public void Player2Hands_DealAndHit_CorrectInstanceValues()
		{
			var playerId = "8625cf04-b7e2";
			var playerOne = new BlackJackPlayer(new KeyValuePair<string, Avitar>(playerId, player1), HandIdentifierProvider, 2);
			var blkJkJackClubs = new BlackJackCard(jackClubs, true);
			var handId = new GuidBasedHandIdentifierProviderMock().GenerateHandIds(1).Single();

			playerOne.DealHands(twoThreeClubs);
			Assert.AreEqual(PlayerStatusTypes.InProgress, playerOne.Status);

			playerOne.Hit(handId, blkJkJackClubs);

			Assert.AreEqual(true, playerOne.Hands.Single(h => h.Identifier == handId).Cards.ElementAt(0).FaceDown);
			Assert.AreEqual(false, playerOne.Hands.Single(h => h.Identifier == handId).Cards.ElementAt(1).FaceDown);
			Assert.AreEqual(false, playerOne.Hands.Single(h => h.Identifier == handId).Cards.ElementAt(2).FaceDown);
			Assert.AreEqual(15, playerOne.Hands.Single(h => h.Identifier == handId).PointValue);
			Assert.AreEqual(PlayerStatusTypes.InProgress, playerOne.Status);
			Assert.AreEqual(playerId, playerOne.Identifier);
			Assert.AreEqual(2, playerOne.Hands.Count());
			Assert.AreEqual(HandStatusTypes.InProgress, playerOne.Hands.Single(h => h.Identifier == handId).Status);
			// TODO Test Second Hand Values
		}

		[Test]
		public void Player2Hands_DealAndHitAndHold_CorrectInstanceValues()
		{
			var playerId = "8625cf04-b7e2";
			var playerOne = new BlackJackPlayer(new KeyValuePair<string, Avitar>(playerId, player1), HandIdentifierProvider, 2);
			var blkJkJackClubs = new BlackJackCard(jackClubs, true);
			var handId = new GuidBasedHandIdentifierProviderMock().GenerateHandIds(1).Single();

			playerOne.DealHands(twoThreeClubs);
			Assert.AreEqual(PlayerStatusTypes.InProgress, playerOne.Status);

			playerOne.Hit(handId, blkJkJackClubs);

			Assert.AreEqual(true, playerOne.Hands.Single(h => h.Identifier == handId).Cards.ElementAt(0).FaceDown);
			Assert.AreEqual(false, playerOne.Hands.Single(h => h.Identifier == handId).Cards.ElementAt(1).FaceDown);
			Assert.AreEqual(false, playerOne.Hands.Single(h => h.Identifier == handId).Cards.ElementAt(2).FaceDown);
			Assert.AreEqual(15, playerOne.Hands.Single(h => h.Identifier == handId).PointValue);
			Assert.AreEqual(PlayerStatusTypes.InProgress, playerOne.Status);
			Assert.AreEqual(playerId, playerOne.Identifier);
			Assert.AreEqual(2, playerOne.Hands.Count());
			Assert.AreEqual(HandStatusTypes.InProgress, playerOne.Hands.Single(h => h.Identifier == handId).Status);
			// TODO Test Second Hand Values

			playerOne.Hold(handId);

			Assert.AreEqual(true, playerOne.Hands.Single(h => h.Identifier == handId).Cards.ElementAt(0).FaceDown);
			Assert.AreEqual(false, playerOne.Hands.Single(h => h.Identifier == handId).Cards.ElementAt(1).FaceDown);
			Assert.AreEqual(false, playerOne.Hands.Single(h => h.Identifier == handId).Cards.ElementAt(2).FaceDown);
			Assert.AreEqual(15, playerOne.Hands.Single(h => h.Identifier == handId).PointValue);
			Assert.AreEqual(PlayerStatusTypes.InProgress, playerOne.Status);
			Assert.AreEqual(playerId, playerOne.Identifier);
			Assert.AreEqual(2, playerOne.Hands.Count());
			Assert.AreEqual(HandStatusTypes.Hold, playerOne.Hands.Single(h => h.Identifier == handId).Status);
			// TODO Test Second Hand Values
		}

		[Test]
		public void Player_HoldsCard_PlayerStatusComplete()
		{
			var playerOne = new BlackJackPlayer(new KeyValuePair<string, Avitar>("8625cf04-b7e2", player1), HandIdentifierProvider, 1);

			var blkJkTwoClubs = new BlackJackCard(twoClubs, true);
			var blkJkThreeClubs = new BlackJackCard(threeClubs, false);
			var blkJkJackClubs = new BlackJackCard(jackClubs, true);
			var handId = new GuidBasedHandIdentifierProviderMock().GenerateHandIds(1).Single();

			playerOne.Hit(handId, blkJkTwoClubs);
			playerOne.Hit(handId, blkJkThreeClubs);
			playerOne.Hit(handId, blkJkJackClubs);
			playerOne.Hold(handId);

			Assert.AreEqual(PlayerStatusTypes.Complete, playerOne.Status);
		}

		[Test]
		public void Player2Hands_HoldsOneHand_PlayerStatusInProgress()
		{
			var playerOne = new BlackJackPlayer(new KeyValuePair<string, Avitar>("8625cf04-b7e2", player1), HandIdentifierProvider, 2);

			var blkJkJackClubs = new BlackJackCard(jackClubs, true);
			var handId = new GuidBasedHandIdentifierProviderMock().GenerateHandIds(1).Single();

			playerOne.DealHands(twoThreeClubs);
			playerOne.Hit(handId, blkJkJackClubs);
			playerOne.Hold(handId);

			Assert.AreEqual(PlayerStatusTypes.InProgress, playerOne.Status);
		}
	}
}
