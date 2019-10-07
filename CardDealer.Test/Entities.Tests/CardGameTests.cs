using Interactors.Mocks;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Entities.Tests
{
    public class CardGameTests
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
        }

        //[Test]
        //public void NewGame_SinglePlayerAndDealer_CurrentPlayerIndexZero()
        //{
        //    var players = new List<string>() {
        //        playerName,
        //    };
        //    var game = new CardGame(deck, players, "");
        //    Assert.AreEqual(players.First(), game.CurrentPlayer.Name);
        //}

        //[Test]
        //public void NewGame_SinglePlayerAndDealer_CardCount52()
        //{
        //    var players = new List<string>() {
        //        playerName,
        //    };
        //    var game = new CardGame(deck, players, "");
        //    Assert.AreEqual(52, game.Deck.Count);
        //}

        //[Test]
        //public void NewGame_SinglePlayerAndDealer_DealerNameJeff()
        //{
        //    var players = new List<string>() {
        //        playerName,
        //    };
        //    var dealerName = "Jeff";
        //    var game = new CardGame(deck, players, dealerName);
        //    Assert.AreEqual(dealerName, game.Players.Last().Name);
        //}

        //[Test]
        //public void NewGame_SinglePlayerAndDealer_DealerCardCountZero()
        //{
        //    var players = new List<string>() {
        //        playerName,
        //    };
        //    var dealerName = "Jeff";
        //    var game = new CardGame(deck, players, dealerName);
        //    Assert.AreEqual(0, game.Players.Last().Hand.Count);
        //}
    }
}
