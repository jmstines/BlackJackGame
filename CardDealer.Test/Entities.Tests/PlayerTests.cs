using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Entities.Tests
{
    public class PlayerTests
    {
        //private readonly Card twoClubs = new Card(CardSuit.Clubs, "2", "2");
        //private readonly Card threeClubs = new Card(CardSuit.Clubs, "3", "3");
        //private readonly Card jackClubs = new Card(CardSuit.Clubs, "J", "Jack");
        //private const string playerName = "Sam";

        //[SetUp]
        //public void Setup()
        //{

        //}

        //[Test]
        //public void NewPlayer_NullName_ArgumentNullException()
        //{
        //    var hand = new List<Card>
        //    {
        //        twoClubs,
        //        threeClubs
        //    };
        //    Assert.Throws<ArgumentNullException>(() => new Player(null));
        //}

        //[Test]
        //public void NewPlayer_NoCards_ZeroPoints()
        //{
        //    Player sam = new Player(playerName);
        //    Assert.AreEqual(0, sam.PointTotal);
        //}

        //[Test]
        //public void NewPlayer_NoCards_HandEmpty()
        //{
        //    var sam = new Player(playerName);
        //    Assert.AreEqual(false, sam.Hand.Any());
        //}

        //[Test]
        //public void NewPlayer_NoCards_StatusInProgress()
        //{
        //    var sam = new Player(playerName);
        //    Assert.AreEqual(PlayerStatus.InProgress, sam.Status);
        //}

        //[Test]
        //public void NewPlayer_CalculateTotal_TotalFive()
        //{
        //    var sam = new Player(playerName);
        //    var blkJkTwoClubs = new BlackJackCard(twoClubs, true, 2);
        //    var blkJkThreeClubs = new BlackJackCard(threeClubs, true, 3);
        //    sam.AddCardToHand(blkJkTwoClubs);
        //    sam.AddCardToHand(blkJkThreeClubs);
        //    Assert.AreEqual(5, sam.PointTotal);
        //}

        //[Test]
        //public void Player_DrawCard_ArgumentNullException()
        //{
        //    var playerOne = new Player(playerName);

        //    Assert.Throws<ArgumentNullException>(() => playerOne.AddCardToHand(null));
        //}

        //[Test]
        //public void Player_DrawCard_TotalFifteen()
        //{
        //    var playerOne = new Player(playerName);
        //    var blkJkTwoClubs = new BlackJackCard(twoClubs, true, 2);
        //    var blkJkThreeClubs = new BlackJackCard(threeClubs, true, 3);
        //    var blkJkJackClubs = new BlackJackCard(jackClubs, true, 10);
        //    playerOne.AddCardToHand(blkJkTwoClubs);
        //    playerOne.AddCardToHand(blkJkThreeClubs);
        //    playerOne.AddCardToHand(blkJkJackClubs);

        //    Assert.AreEqual(15, playerOne.PointTotal);
        //}

        //[Test]
        //public void Player_DrawCard_FirstCardFaceDown()
        //{
        //    var playerOne = new Player(playerName);
        //    var blkJkTwoClubs = new BlackJackCard(twoClubs, true, 2);
        //    playerOne.AddCardToHand(blkJkTwoClubs);

        //    Assert.AreEqual(true, playerOne.Hand.First().FaceDown);
        //}

        //[Test]
        //public void Player_DrawCard_SecondCardFaceUp()
        //{
        //    var playerOne = new Player(playerName);
        //    var blkJkTwoClubs = new BlackJackCard(twoClubs, true, 2);
        //    var blkJkThreeClubs = new BlackJackCard(threeClubs, false, 3);
        //    playerOne.AddCardToHand(blkJkTwoClubs);
        //    playerOne.AddCardToHand(blkJkThreeClubs);

        //    Assert.AreEqual(false, playerOne.Hand[1].FaceDown);
        //}

        //[Test]
        //public void Player_DrawCard_ThirdCardFaceUp()
        //{
        //    var playerOne = new Player(playerName);
        //    var blkJkTwoClubs = new BlackJackCard(twoClubs, true, 2);
        //    var blkJkThreeClubs = new BlackJackCard(threeClubs, false, 3);
        //    var blkJkJackClubs = new BlackJackCard(jackClubs, true, 10);
        //    playerOne.AddCardToHand(blkJkTwoClubs);
        //    playerOne.AddCardToHand(blkJkThreeClubs);
        //    playerOne.AddCardToHand(blkJkJackClubs);

        //    Assert.AreEqual(true, playerOne.Hand[2].FaceDown);
        //}
    }
}
