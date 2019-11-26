using Entities.Enums;
using Interactors.Mocks;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Entities.Tests
{
	public class BlackJackGameTests
	{
		private const string playerName = "Sam";
		private const string playerName2 = "Tom";

		[Test]
		public void NewGame_NullPlayer_ArgumentNullException()
		{
			var players = new List<string>() {
				playerName,
				playerName2
			};
			Assert.Throws<ArgumentNullException>(() => new BlackJackGame(4).AddPlayer(null));
		}

		[Test]
		public void NewGame_NullDeck_PlayerArgumentNullException()
		{
			var game = new BlackJackGame(4);
			var player = new Player(playerName);
			game.AddPlayer(new BlackJackPlayer("8625cf04-b7e2", player));
			game.AddPlayer(new BlackJackPlayer("8625cf04-b7e3", player));
			game.AddPlayer(new BlackJackPlayer("8625cf04-b7e2", player));
			game.AddPlayer(new BlackJackPlayer("8625cf04-b7e3", player));

			Assert.Throws<InvalidOperationException>(() => game.AddPlayer(new BlackJackPlayer("8625cf04-b7e4", player)));
		}

		[Test]
		 public void NewGame_SinglePlayer_PlayerCountOne()
		{
			var game = new BlackJackGame(4);
			var player = new Player(playerName);
			game.AddPlayer(new BlackJackPlayer("8625cf04-b7e2", player));
			Assert.AreEqual(1, game.Players.Count());
			Assert.AreEqual(GameStatus.Waiting, game.Status);
		}

		[Test]
		public void NewGame_FullGame_AutoStarts()
		{
			var game = new BlackJackGame(4);
			var player = new Player(playerName);
			game.AddPlayer(new BlackJackPlayer("8625cf04-b7e2", player));
			game.AddPlayer(new BlackJackPlayer("8625cf04-b7e3", player));
			game.AddPlayer(new BlackJackPlayer("8625cf04-b7e4", player));
			game.AddPlayer(new BlackJackPlayer("8625cf04-b7e5", player));
			Assert.AreEqual(4, game.Players.Count());
			Assert.AreEqual(GameStatus.InProgress, game.Status);
		}

		[Test]
		public void NewGame_SinglePlayerAndDealer_CurrentPlayerIndexZero()
		{
			var game = new BlackJackGame(4);
			var player = new Player(playerName);
			var player2 = new Player(playerName2);
			game.AddPlayer(new BlackJackPlayer("8625cf04-b7e2", player));
			game.AddPlayer(new BlackJackPlayer("8625cf04-b7e2", player2));
			Assert.AreEqual(playerName, game.CurrentPlayer.Name);
		}


		//[Test]
		//public void Deal_NullDeck_ThrowArgumentNullException()
		//{
		//	var dealerName = "Jeff";
		//	var game = new BlackJackGame();
		//	var player = new Player(playerName);
		//	var dealer = new Player(dealerName);
		//	game.AddPlayer(new BlackJackPlayer("8625cf04-b7e2", player));
		//	game.AddPlayer(new BlackJackPlayer("8625cf04-b7e3", dealer));
		//	Assert.Throws<ArgumentNullException>(() => game.DealHands(null));
		//}

		//[Test]
		//public void Deal_ThenAddNewPlayer_ThrowInvalidOperationException()
		//{
		//	var dealerName = "Jeff";
		//	var game = new BlackJackGame();
		//	var player = new Player(playerName);
		//	var dealer = new Player(dealerName);
		//	game.AddPlayer(new BlackJackPlayer("8625cf04-b7e2", player));
		//	game.AddPlayer(new BlackJackPlayer("8625cf04-b7e3", dealer));
		//	game.DealHands(deck);
		//	Assert.Throws<InvalidOperationException>(() => game.AddPlayer(new BlackJackPlayer("8625cf04-b7e4", player)));
		//}

		//[Test]
		//public void AfterDeal_DeckOutOfCards_ThrowArgumentOutOfRangeException()
		//{
		//	var dealerName = "Jeff";
		//	var game = new BlackJackGame();
		//	var currentDeck = deck.Take(4);
		//	var player = new Player(playerName);
		//	var dealer = new Player(dealerName);
		//	game.AddPlayer(new BlackJackPlayer("8625cf04-b7e2", player));
		//	game.AddPlayer(new BlackJackPlayer("8625cf04-b7e3", dealer));
		//	game.DealHands(currentDeck);
		//	Assert.Throws<ArgumentOutOfRangeException>(() => game.PlayerHits());
		//}

		//[Test]
		//public void Deal_SinglePlayerAndDealer_CurrectValues()
		//{
		//	var dealerName = "Jeff";
		//	var game = new BlackJackGame();
		//	var player = new Player(playerName);
		//	var dealer = new Player(dealerName);
		//	game.AddPlayer(new BlackJackPlayer("8625cf04-b7e2", player));
		//	game.AddPlayer(new BlackJackPlayer("8625cf04-b7e3", dealer));
		//	game.DealHands(deck);
		//	Assert.AreEqual(2, game.Players.Last().Hand.Cards.Count());
		//	Assert.AreEqual(6, game.Players.First().Hand.PointValue);
		//	Assert.AreEqual(8, game.Players.Last().Hand.PointValue);
		//	Assert.IsTrue(game.Players.First().Hand.Cards.First().FaceDown);
		//	Assert.IsFalse(game.Players.First().Hand.Cards.Last().FaceDown);
		//	Assert.AreEqual(48, game.Deck.Count());
		//}

		//[Test]
		//public void AfterDeal_PlayerHits_CurrectValues()
		//{
		//	var dealerName = "Jeff";
		//	var game = new BlackJackGame();
		//	var player = new Player(playerName);
		//	var dealer = new Player(dealerName);
		//	game.AddPlayer(new BlackJackPlayer("8625cf04-b7e2", player));
		//	game.AddPlayer(new BlackJackPlayer("8625cf04-b7e3", dealer));
		//	game.DealHands(deck);
		//	game.PlayerHits();
		//	Assert.AreEqual(2, game.Players.Last().Hand.Cards.Count());
		//	Assert.AreEqual(12, game.Players.First().Hand.PointValue);
		//	Assert.AreEqual(8, game.Players.Last().Hand.PointValue);
		//	Assert.IsTrue(game.Players.First().Hand.Cards.First().FaceDown);
		//	Assert.IsFalse(game.Players.First().Hand.Cards.Last().FaceDown);
		//	Assert.AreEqual(47, game.Deck.Count());
		//}

		//[Test]
		//public void AfterDeal_PlayerHolds_DealerCurrentPlayer()
		//{
		//	var dealerName = "Jeff";
		//	var game = new BlackJackGame();
		//	var player = new Player(playerName);
		//	var dealer = new Player(dealerName);
		//	game.AddPlayer(new BlackJackPlayer("8625cf04-b7e2", player));
		//	game.AddPlayer(new BlackJackPlayer("8625cf04-b7e3", dealer));
		//	game.DealHands(deck);
		//	game.PlayerHolds();

		//	Assert.AreEqual(dealerName, game.CurrentPlayer.Name);
		//}
	}
}
