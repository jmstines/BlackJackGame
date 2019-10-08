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
        private readonly IEnumerable<Card> deck = new CardDeckProviderMock().Deck;

        [Test]
        public void NewGame_NullPlayer_ArgumentNullException()
        {
            var players = new List<string>() {
                playerName,
                playerName2
            };
            Assert.Throws<ArgumentNullException>(() => new BlackJackGame().AddPlayer(null));
        }

        public void NewGame_NullDeck_PlayerArgumentNullException()
        {
			var game = new BlackJackGame();
			game.AddPlayer(new Player(playerName));
			game.AddPlayer(new Player(playerName));

			Assert.Throws<ArgumentOutOfRangeException>(() => game.AddPlayer(new Player(playerName)));
        }

        [Test]
        public void NewGame_TooManyPlayers_ArgumentOutOfRangeException()
        {
			var game = new BlackJackGame();
			game.AddPlayer(new Player(playerName));
			game.AddPlayer(new Player(playerName));
			game.AddPlayer(new Player(playerName));
			game.AddPlayer(new Player(playerName));
			game.AddPlayer(new Player(playerName));
			Assert.Throws<ArgumentOutOfRangeException>(() => game.AddPlayer(new Player(playerName)));
		}

        [Test]
        public void NewGame_SinglePlayer_PlayerCountOne()
        {
            var game = new BlackJackGame();
			game.AddPlayer(new Player(playerName));
            Assert.AreEqual(1, game.Players.Count());
			Assert.AreEqual(0, game.Players.First().PlayerIndex);
			Assert.AreEqual(GameStatus.Waiting, game.Status);
        }

		public void NewGame_FullGame_AutoStarts()
		{
			var game = new BlackJackGame();
			game.AddPlayer(new Player(playerName));
			game.AddPlayer(new Player(playerName));
			game.AddPlayer(new Player(playerName));
			game.AddPlayer(new Player(playerName));
			game.AddPlayer(new Player(playerName));
			Assert.AreEqual(5, game.Players.Count());
			Assert.AreEqual(4, game.Players.Last().PlayerIndex);
			Assert.AreEqual(GameStatus.InProgress, game.Status);
		}

		[Test]
		public void NewGame_SinglePlayerAndDealer_CurrentPlayerIndexZero()
		{
			var game = new BlackJackGame();
			game.AddPlayer(new Player(playerName));
			game.AddPlayer(new Player(playerName2));
			Assert.AreEqual(playerName, game.CurrentPlayer.Name);
			Assert.AreEqual(0, game.CurrentPlayer.PlayerIndex);
		}

		[Test]
		public void NewGame_NoPlayers_CardCountZero()
		{
			var game = new BlackJackGame();
			Assert.AreEqual(0, game.Deck.Count());
		}

		[Test]
		public void NewGame_SinglePlayerAndDealer_DealerCardCountZero()
		{
			var dealerName = "Jeff";
			var game = new BlackJackGame();
			game.AddPlayer(new Player(playerName));
			game.AddPlayer(new Player(dealerName));
			game.DealHands(deck);
			Assert.AreEqual(2, game.Players.Last().Hand.Cards.Count());
		}
	}
}
