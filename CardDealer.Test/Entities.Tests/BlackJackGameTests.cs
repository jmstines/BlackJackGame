using CardDealer.Tests.Providers.Mocks;
using Entities.Enums;
using Entities.Interfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Entities.Tests
{
	[TestFixture]
	public class BlackJackGameTests
	{
		private const string playerName = "Sam";
		private const string playerName2 = "Tom";
		private const string DealerDataId = "1234ck64-f9d8";

		private BlackJackPlayer DealerNamedData;

		private readonly Deck cards = new Deck();
		private readonly IRandomProvider random = new RandomProvider();
		private readonly ICardProvider cardProvider;
		private readonly IHandIdentifierProvider HandIdentifierProvider;

		public BlackJackGameTests()
		{
			cardProvider = new CardProviderMock();
			HandIdentifierProvider = new GuidBasedHandIdentifierProviderMock();
		}

		[SetUp]
		public void Init()
		{
			DealerNamedData = new BlackJackPlayer(new KeyValuePair<string, Avitar>(
					"1234ck64-f9d8", new Avitar("Data")), HandIdentifierProvider, 1);
		}

		[Test]
		public void NewGame_NullPlayer_ArgumentNullException()
		{
			var players = new List<string>() {
				playerName,
				playerName2
			};
			Assert.Throws<ArgumentNullException>(() => new BlackJackGame(cardProvider, DealerNamedData, 4).AddPlayer(null));
		}

		[Test]
		public void NewGame_NullCardProvider_ArgumentNullException()
		{
			var players = new List<string>() {
				playerName,
				playerName2
			};
			Assert.Throws<ArgumentNullException>(() => new BlackJackGame(null, DealerNamedData, 4));
		}

		[Test]
		public void NewGame_NullDealer_ArgumentNullException()
		{
			var players = new List<string>() {
				playerName,
				playerName2
			};
			Assert.Throws<ArgumentNullException>(() => new BlackJackGame(cardProvider, null, 4));
		}

		[Test]
		public void NewGame_ZeroHandCount_ArgumentOutOfRangeException()
		{
			var players = new List<string>() {
				playerName,
				playerName2
			};
			Assert.Throws<ArgumentOutOfRangeException>(() => new BlackJackGame(cardProvider, DealerNamedData, 0));
		}

		[Test]
		public void NewGame_NullDeck_PlayerArgumentNullException()
		{
			var game = new BlackJackGame(cardProvider, DealerNamedData ,4);
			var player = new Avitar(playerName);
			game.AddPlayer(new BlackJackPlayer(new KeyValuePair<string, Avitar>("8625cf04-b7e2", player), HandIdentifierProvider, 1));
			game.AddPlayer(new BlackJackPlayer(new KeyValuePair<string, Avitar>("8625cf04-b7e2", player), HandIdentifierProvider, 1));
			game.AddPlayer(new BlackJackPlayer(new KeyValuePair<string, Avitar>("8625cf04-b7e2", player), HandIdentifierProvider, 1));
			game.AddPlayer(new BlackJackPlayer(new KeyValuePair<string, Avitar>("8625cf04-b7e2", player), HandIdentifierProvider, 1));

			Assert.Throws<InvalidOperationException>(() => game.AddPlayer(
				new BlackJackPlayer(new KeyValuePair<string, Avitar>("8625cf04-b7e2", player), HandIdentifierProvider, 1)));
		}

		[Test]
		public void NewGame_SinglePlayer_PlayerCountOne()
		{
			var game = new BlackJackGame(cardProvider, DealerNamedData, 1);
			var player = new Avitar(playerName);
			game.AddPlayer(new BlackJackPlayer(new KeyValuePair<string, Avitar>("8625cf04-b7e2", player), HandIdentifierProvider, 1));
			Assert.AreEqual(2, game.Players.Count());
			Assert.AreEqual(GameStatus.Ready, game.Status);
		}

		[Test]
		public void NewGame_FullGame_AutoSetToReady()
		{
			var game = new BlackJackGame(cardProvider, DealerNamedData, 4);
			var player = new Avitar(playerName);
			game.AddPlayer(new BlackJackPlayer(new KeyValuePair<string, Avitar>("8625cf04-b7e2", player), HandIdentifierProvider, 1));
			game.AddPlayer(new BlackJackPlayer(new KeyValuePair<string, Avitar>("8625cf04-b7e2", player), HandIdentifierProvider, 1));
			game.AddPlayer(new BlackJackPlayer(new KeyValuePair<string, Avitar>("8625cf04-b7e2", player), HandIdentifierProvider, 1));
			game.AddPlayer(new BlackJackPlayer(new KeyValuePair<string, Avitar>("8625cf04-b7e2", player), HandIdentifierProvider, 1));
			Assert.AreEqual(5, game.Players.Count());
			Assert.AreEqual(GameStatus.Ready, game.Status);
		}

		[Test]
		public void NewGame_SinglePlayerAndDealer_CurrentPlayerIndexZero()
		{
			var game = new BlackJackGame(cardProvider, DealerNamedData, 4);
			var player = new Avitar(playerName);
			var player2 = new Avitar(playerName2);

			game.AddPlayer(new BlackJackPlayer(new KeyValuePair<string, Avitar>("8625cf04-b7e2", player), HandIdentifierProvider, 1));
			game.AddPlayer(new BlackJackPlayer(new KeyValuePair<string, Avitar>("8625cf04-b7e2", player2), HandIdentifierProvider, 1));
			Assert.AreEqual(playerName, game.CurrentPlayer.Name);
			Assert.AreEqual(2, game.Players.Count());
			Assert.AreEqual(GameStatus.Waiting, game.Status);
		}


		[Test]
		public void Deal_NullDeck_ThrowArgumentNullException()
		{
			var game = new BlackJackGame(cardProvider, DealerNamedData, 4);
			var player = new Avitar(playerName);
			var player2 = new Avitar(playerName2);

			game.AddPlayer(new BlackJackPlayer(new KeyValuePair<string, Avitar>("8625cf04-b7e2", player), HandIdentifierProvider, 1));
			game.AddPlayer(new BlackJackPlayer(new KeyValuePair<string, Avitar>("8625cf04-b7e2", player2), HandIdentifierProvider, 1));
			Assert.Throws<ArgumentOutOfRangeException>(() => game.DealHands());
		}

		[Test]
		public void Deal_ThenAddNewPlayer_ThrowInvalidOperationException()
		{
			var game = new BlackJackGame(cardProvider, DealerNamedData, 2);
			var player = new Avitar(playerName);
			var player2 = new Avitar(playerName2);

			game.AddPlayer(new BlackJackPlayer(new KeyValuePair<string, Avitar>("8625cf04-b7e2", player), HandIdentifierProvider, 1));
			game.AddPlayer(new BlackJackPlayer(new KeyValuePair<string, Avitar>("8625cf04-b7e2", player2), HandIdentifierProvider, 1));
			game.DealHands();
			Assert.Throws<InvalidOperationException>(() =>
				game.AddPlayer(new BlackJackPlayer(new KeyValuePair<string, Avitar>("8625cf04-b7e2", player), HandIdentifierProvider, 1))
			);
		}

		[Test]
		public void Deal_SinglePlayerAndDealer_CurrectValues()
		{
			var game = new BlackJackGame(cardProvider, DealerNamedData, 2);
			var player = new Avitar(playerName);
			var player2 = new Avitar(playerName2);

			game.AddPlayer(new BlackJackPlayer(new KeyValuePair<string, Avitar>("8625cf04-b7e2", player), HandIdentifierProvider, 1));
			game.AddPlayer(new BlackJackPlayer(new KeyValuePair<string, Avitar>("8625cf04-b7e2", player2), HandIdentifierProvider, 1));
			game.DealHands();
			Assert.AreEqual(GameStatus.InProgress, game.Status);
			Assert.AreEqual(2, game.Players.Last().Hands.First().Cards.Count());
			Assert.AreEqual(5, game.Players.First().Hands.First().PointValue);
			Assert.AreEqual(5, game.Players.Skip(1).Take(1).Single().Hands.Single().PointValue);
			Assert.AreEqual(5, game.Players.Last().Hands.First().PointValue);
			Assert.IsTrue(game.Players.First().Hands.First().Cards.First().FaceDown);
			Assert.IsFalse(game.Players.First().Hands.First().Cards.Last().FaceDown);
			Assert.IsTrue(game.Players.Skip(1).Take(1).Single().Hands.First().Cards.First().FaceDown);
			Assert.IsFalse(game.Players.Skip(1).Take(1).Single().Hands.First().Cards.Last().FaceDown);
			Assert.IsTrue(game.Players.Skip(2).Take(1).Single().Hands.First().Cards.First().FaceDown);
			Assert.IsFalse(game.Players.Skip(2).Take(1).Single().Hands.First().Cards.Last().FaceDown);
		}

		[Test]
		public void AfterDeal_PlayerHits_CurrectValues()
		{
			var playerOneId = "8625cf04-b7e2";
			var game = new BlackJackGame(cardProvider, DealerNamedData, 1);
			var player2 = new Avitar(playerName2);

			game.AddPlayer(new BlackJackPlayer(new KeyValuePair<string, Avitar>(playerOneId, player2), HandIdentifierProvider, 1));
			game.DealHands();
			Assert.AreEqual(2, game.Players.Last().Hands.First().Cards.Count());
			Assert.AreEqual(5, game.Players.First().Hands.First().PointValue);
			Assert.AreEqual(5, game.Players.Last().Hands.First().PointValue);
			Assert.IsTrue(game.Players.First().Hands.First().Cards.First().FaceDown);
			Assert.IsFalse(game.Players.First().Hands.First().Cards.Last().FaceDown);
			Assert.IsTrue(game.Players.Last().Hands.First().Cards.First().FaceDown);
			Assert.IsFalse(game.Players.Last().Hands.First().Cards.Last().FaceDown);

			game.PlayerHits(playerOneId, HandIdentifierProvider.GenerateHandIds(1).Single());
			Assert.AreEqual(3, game.Players.First().Hands.First().Cards.Count());
			Assert.AreEqual(7, game.Players.First().Hands.First().PointValue);
		}

		[Test]
		public void AfterDeal_PlayerHolds_CurrectValues()
		{
			var playerOneId = "8625cf04-b7e2";
			var game = new BlackJackGame(cardProvider, DealerNamedData, 1);
			var player2 = new Avitar(playerName2);

			game.AddPlayer(new BlackJackPlayer(new KeyValuePair<string, Avitar>(playerOneId, player2), HandIdentifierProvider, 1));
			game.DealHands();
			Assert.AreEqual(2, game.Players.Last().Hands.First().Cards.Count());
			Assert.AreEqual(5, game.Players.First().Hands.First().PointValue);
			Assert.AreEqual(5, game.Players.Last().Hands.First().PointValue);
			Assert.IsTrue(game.Players.First().Hands.First().Cards.First().FaceDown);
			Assert.IsFalse(game.Players.First().Hands.First().Cards.Last().FaceDown);
			Assert.IsTrue(game.Players.Last().Hands.First().Cards.First().FaceDown);
			Assert.IsFalse(game.Players.Last().Hands.First().Cards.Last().FaceDown);
			Assert.AreEqual(GameStatus.InProgress, game.Status);

			game.PlayerHolds(playerOneId, HandIdentifierProvider.GenerateHandIds(1).Single());
			Assert.AreEqual(2, game.Players.First().Hands.First().Cards.Count());
			Assert.AreEqual(5, game.Players.First().Hands.First().PointValue);
			Assert.AreEqual(PlayerStatusTypes.Complete, game.Players.First().Status);
			Assert.AreEqual(PlayerStatusTypes.InProgress, game.Players.Last().Status);
			Assert.AreEqual(GameStatus.InProgress, game.Status);

			game.PlayerHolds(DealerDataId, HandIdentifierProvider.GenerateHandIds(1).Single());
			Assert.AreEqual(3, game.Players.Last().Hands.First().Cards.Count());
			Assert.AreEqual(7, game.Players.Last().Hands.First().PointValue);
			Assert.AreEqual(PlayerStatusTypes.Complete, game.Players.First().Status);
			Assert.AreEqual(PlayerStatusTypes.InProgress, game.Players.Last().Status);
			Assert.AreEqual(GameStatus.Complete, game.Status);
		}
	}
}
