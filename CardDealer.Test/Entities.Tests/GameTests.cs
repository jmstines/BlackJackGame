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

      List<Card> deck = new CardDeckProvider().Deck;
      Assert.Throws<ArgumentOutOfRangeException>(() => new Game(deck, players));      
    }

    [Test]
    public void NewGame_NotEnoughPlayers_ArgumentOutOfRangeException()
    {
      List<Player> players = new List<Player>() {};

      List<Card> deck = new CardDeckProvider().Deck;
      Assert.Throws<ArgumentOutOfRangeException>(() => new Game(deck, players));
    }

    [Test]
    public void NewGame_NullPlayerList_ArgumentNullException()
    {
      List<Card> deck = new CardDeckProvider().Deck;

      Assert.Throws<ArgumentNullException>(() => new Game(deck, null));
    }

    [Test]
    public void NewGame_NewGame_GameCreatesDealerPlayer()
    {
      List<Player> players = new List<Player>() {
        new Player(playerName),
        new Player(playerName2)
      };
      List<Card> deck = new CardDeckProvider().Deck;
      Game game = new Game(deck, players);

      Assert.AreEqual("Dealer", game.Dealer.Name);
      Assert.AreEqual(2, game.Dealer.Hand.Count);
      Assert.AreEqual(13, game.Dealer.PointTotal);
    }
  }
}
