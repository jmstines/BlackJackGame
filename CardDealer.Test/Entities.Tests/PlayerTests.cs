using Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
  public class PlayerTests
  {
    private readonly Card twoClubs = new Card(Suit.Clubs, "2", "2", 2);
    private readonly Card threeClubs = new Card(Suit.Clubs, "3", "3", 3);
    private readonly Card jackClubs = new Card(Suit.Clubs, "J", "Jack", 10);
    private const string playerName = "Sam";

    [SetUp]
    public void Setup()
    {

    }

    [Test]
    public void NewPlayer_NullName_ArgumentNullException()
    {
      var hand = new List<Card>
      {
        twoClubs,
        threeClubs
      };
      Assert.Throws<ArgumentNullException>(() => new Player(null));
    }

    [Test]
    public void NewPlayer_CalculateTotal_TotalFive()
    {
      Player sam = new Player(playerName);
      sam.DrawCard(twoClubs);
      sam.DrawCard(threeClubs);
      Assert.AreEqual(5 , sam.PointTotal);
    }

    [Test]
    public void Player_DrawCard_ArgumentNullException()
    {
      var playerOne = new Player(playerName);

      Assert.Throws<ArgumentNullException>(() => playerOne.DrawCard(null));
    }

    [Test]
    public void Player_DrawCard_TotalFifteen()
    {
      var playerOne = new Player(playerName);
      playerOne.DrawCard(twoClubs);
      playerOne.DrawCard(threeClubs);
      playerOne.DrawCard(jackClubs);

      Assert.AreEqual(15, playerOne.PointTotal);
    }

    [Test]
    public void Player_DrawCard_FirstCardFaceDown()
    {
      var playerOne = new Player(playerName);
      playerOne.DrawCard(twoClubs);

      Assert.AreEqual(CardOrientation.FaceDown, playerOne.Hand.First().Orientation);
    }

    [Test]
    public void Player_DrawCard_SecondCardFaceUp()
    {
      var playerOne = new Player(playerName);
      playerOne.DrawCard(twoClubs);
      playerOne.DrawCard(threeClubs);

      Assert.AreEqual(CardOrientation.FaceUp, playerOne.Hand[1].Orientation);
    }

    [Test]
    public void Player_DrawCard_ThirdCardFaceUp()
    {
      var playerOne = new Player(playerName);
      playerOne.DrawCard(twoClubs);
      playerOne.DrawCard(threeClubs);
      playerOne.DrawCard(jackClubs);

      Assert.AreEqual(CardOrientation.FaceUp, playerOne.Hand[2].Orientation);
    }
  }
}
