using Interactors.Mocks;
using Entities;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System;


namespace Interactors.Tests
{
    public class BackJackGameTests
    {
        //private const string playerName = "Sam";
        //private const string playerName2 = "Tom";
        //private const string playerName3 = "Jim";
        //private const string playerName4 = "Matt";

        //private readonly IEnumerable<Card> deck = new CardDeckProviderMock().Deck;
        //private readonly List<string> players4 = new List<string>() {
        //    playerName, 
        //    playerName2,
        //    playerName3,
        //    playerName4
        //};
        //private readonly List<string> players2 = new List<string>() {
        //    playerName,
        //    playerName2
        //};
        //private readonly List<string> players1 = new List<string>() {
        //    playerName
        //};

        //[SetUp]
        //public void Setup()
        //{

        //}

        //[Test]
        //public void NewGame_NewGame_GameCreatesDefaultGame()
        //{
        //    CardGame cardGame = new CardGame(deck, players2, "");
        //    BlackJackGame game = new BlackJackGame(cardGame);

        //    Assert.AreEqual("Dealer", game.Game.Players.Last().Name);
        //    Assert.AreEqual(2, game.Game.Players.Last().Hand.Count);
        //    Assert.AreEqual(11, game.Game.Players.Last().PointTotal);
        //    Assert.AreEqual(playerName, game.Game.Players[0].Name);
        //    Assert.AreEqual(2, game.Game.Players[0].Hand.Count);
        //    Assert.AreEqual(7, game.Game.Players[0].PointTotal);
        //    Assert.AreEqual(playerName2, game.Game.Players[1].Name);
        //    Assert.AreEqual(2, game.Game.Players[1].Hand.Count);
        //    Assert.AreEqual(9, game.Game.Players[1].PointTotal);
        //}

        //[Test]
        //public void NewGame_PlayerDrawsMoreCardsThanInDeck_ArgumentOutOfRangeException()
        //{
        //    CardGame cardGame = new CardGame(new CardDeckProviderMock().Deck_GameOne(), players4, "");
        //    Assert.Throws<ArgumentOutOfRangeException>(() => new BlackJackGame(cardGame));
        //}

        //[Test]
        //public void NewGame_Dealer21onDeal_PlayersLose()
        //{
        //    IEnumerable<Card> deck = new CardDeckProviderMock().Deck_DealerWins();
        //    CardGame cardGame = new CardGame(deck, players2, "");
        //    BlackJackGame game = new BlackJackGame(cardGame);

        //    Assert.IsTrue(game.Game.GameComplete);
        //    Assert.AreEqual(PlayerStatus.PlayerLoses, game.Game.Players.First().Status);
        //    Assert.AreEqual(PlayerStatus.PlayerLoses, game.Game.Players.First().Status);
        //}

        //[Test]
        //public void NewGame_Dealer21onDeal_Player1Ties()
        //{
        //    IEnumerable<Card> deck = new CardDeckProviderMock().GetDeck_DealerAndPlayerOneBlackJack();
        //    CardGame cardGame = new CardGame(deck, players2, "");
        //    BlackJackGame game = new BlackJackGame(cardGame);

        //    Assert.IsTrue(game.Game.GameComplete);
        //    Assert.AreEqual(PlayerStatus.Push, game.Game.Players.First().Status);
        //    Assert.AreEqual(PlayerStatus.PlayerLoses, game.Game.Players[1].Status);
        //}

        //[Test]
        //public void NewGame_Dealer21onDeal_Player1AndPlayer2Tie()
        //{
        //    IEnumerable<Card> deck = new CardDeckProviderMock().Deck_AllPlayersBlackJack();
        //    CardGame cardGame = new CardGame(deck, players2, "");
        //    BlackJackGame game = new BlackJackGame(cardGame);

        //    Assert.IsTrue(game.Game.GameComplete);
        //    Assert.AreEqual(PlayerStatus.Push, game.Game.Players.First().Status);
        //    Assert.AreEqual(PlayerStatus.Push, game.Game.Players[1].Status);
        //}

        //[Test]
        //public void NewGame_Player1Bust_DealerWins()
        //{
        //    IEnumerable<Card> deck = new CardDeckProviderMock().Deck_GameTwo();
        //    CardGame cardGame = new CardGame(deck, players1, "");
        //    BlackJackGame game = new BlackJackGame(cardGame);

        //    game.PlayerAction(PlayerAction.Draw);
        //    Assert.AreEqual(PlayerStatus.PlayerLoses, game.Game.Players[0].Status);
        //    Assert.AreEqual(20, game.Game.Players.Last().PointTotal);
        //    Assert.AreEqual(PlayerStatus.PlayerLoses, game.Game.Players[0].Status);
        //}

        //[Test]
        //public void NewGame_DealeronDeal_Player1Ties()
        //{
        //    List<Player> players = new List<Player>() {
        //        new Player(playerName),
        //        new Player(playerName2)
        //    };
        //    List<Card> blackjackDeck = new List<Card>();
        //    // Can't test till Ace is both 11 and 1
        //}
    }
}
