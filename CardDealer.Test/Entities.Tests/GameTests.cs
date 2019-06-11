using CardDealer.Tests.Entities.Mocks;
using Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CardDealer.Tests.Entities.Tests
{
	public class GameTests
	{
		private const string playerName = "Sam";
		private const string playerName2 = "Tom";
		private readonly List<Card> deck = new CardDeckProviderMock().Deck;

		[SetUp]
		public void Setup()
		{

		}

		[Test]
		public void NewGame_NullDeck_ArgumentNullException()
		{
			List<Player> players = new List<Player>() {
				new Player(playerName),
				new Player(playerName2)
			};
			Assert.Throws<ArgumentNullException>(() => new Game(null, players));
		}

		[Test]
		public void NewGame_TooManyPlayers_ArgumentOutOfRangeException()
		{
			List<Player> players = new List<Player>() {
				new Player(playerName),
				new Player(playerName2),
				new Player(playerName),
				new Player(playerName2),
				new Player(playerName2)
			};
			Assert.Throws<ArgumentOutOfRangeException>(() => new Game(deck, players));
		}

		[Test]
		public void NewGame_NotEnoughPlayers_ArgumentOutOfRangeException()
		{
			List<Player> players = new List<Player>() { };
			Assert.Throws<ArgumentOutOfRangeException>(() => new Game(deck, players));
		}

		[Test]
		public void NewGame_NullPlayerList_ArgumentNullException()
		{
			Assert.Throws<ArgumentNullException>(() => new Game(deck, null));
		}

		[Test]
		public void NewGame_NewGame_GameCreatesDefaultGame()
		{
			List<Player> players = new List<Player>() {
				new Player(playerName),
				new Player(playerName2)
			};
			Game game = new Game(deck, players);

			Assert.AreEqual("Dealer", game.Dealer.Name);
			Assert.AreEqual(2, game.Dealer.Hand.Count);
			Assert.AreEqual(11, game.Dealer.PointTotal);
			Assert.AreEqual(playerName, game.Players[0].Name);
			Assert.AreEqual(2, game.Players[0].Hand.Count);
			Assert.AreEqual(7, game.Players[0].PointTotal);
			Assert.AreEqual(playerName2, game.Players[1].Name);
			Assert.AreEqual(2, game.Players[1].Hand.Count);
			Assert.AreEqual(9, game.Players[1].PointTotal);
		}

		[Test]
		public void NewGame_Dealer21onDeal_PlayersLose()
		{
			List<Player> players = new List<Player>() {
				new Player(playerName),
				new Player(playerName2)
			};
			List<Card> deck = new CardDeckProviderMock().Deck_DealerWins();
			Game game = new Game(deck, players);

			Assert.IsTrue(game.GameComplete);
			Assert.AreEqual(PlayerStatus.PlayerWins, game.Dealer.Status);
			Assert.AreEqual(PlayerStatus.PlayerLoses, game.Players[0].Status);
			Assert.AreEqual(PlayerStatus.PlayerLoses, game.Players[0].Status);
		}

		[Test]
		public void NewGame_Dealer21onDeal_Player1Ties()
		{
			List<Player> players = new List<Player>() {
				new Player(playerName),
				new Player(playerName2)
			};
			List<Card> deck = new CardDeckProviderMock().GetDeck_DealerAndPlayerOneBlackJack();
			Game game = new Game(deck, players);

			Assert.IsTrue(game.GameComplete);
			Assert.AreEqual(PlayerStatus.Push, game.Dealer.Status);
			Assert.AreEqual(PlayerStatus.Push, game.Players[0].Status);
			Assert.AreEqual(PlayerStatus.PlayerLoses, game.Players[1].Status);
		}

		[Test]
		public void NewGame_Dealer21onDeal_Player1AndPlayer2Tie()
		{
			List<Player> players = new List<Player>() {
				new Player(playerName),
				new Player(playerName2)
			};
			List<Card> deck = new CardDeckProviderMock().Deck_AllPlayersBlackJack();
			Game game = new Game(deck, players);

			Assert.IsTrue(game.GameComplete);
			Assert.AreEqual(PlayerStatus.Push, game.Dealer.Status);
			Assert.AreEqual(PlayerStatus.Push, game.Players[0].Status);
			Assert.AreEqual(PlayerStatus.Push, game.Players[1].Status);
		}

		[Test]
		public void NewGame_Player1BustAndPlayer2Wins()
		{
			List<Player> players = new List<Player>() {
				new Player(playerName),
				new Player(playerName2)
			};
			List<Card> deck = new CardDeckProviderMock().Deck_GameOne();
			Game game = new Game(deck, players);

			game.PlayerDrawsCard();
			Assert.AreEqual(PlayerStatus.Hold, game.Players[0].Status);
			Assert.AreSame(players[1], game.CurrentPlayer);
			

			//Assert.AreEqual(Outcome.Push, game.Players[0].Status);
			//Assert.AreEqual(Outcome.Push, game.Players[1].Status);
			//Assert.AreEqual(Outcome.Push, game.Dealer.Status);
		}

		[Test]
		public void NewGame_DealeronDeal_Player1Ties()
		{
			List<Player> players = new List<Player>() {
				new Player(playerName),
				new Player(playerName2)
			};
			List<Card> blackjackDeck = new List<Card>();
			// Can't test till Ace is both 11 and 1
		}
	}
}
