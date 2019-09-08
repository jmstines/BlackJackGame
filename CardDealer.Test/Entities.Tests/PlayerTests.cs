using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Entities.Tests
{
    public class PlayerTests
    {
        private readonly Card twoClubs = new Card(Suit.Clubs, "2", "2");
        private readonly Card threeClubs = new Card(Suit.Clubs, "3", "3");
        private readonly Card jackClubs = new Card(Suit.Clubs, "J", "Jack");
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
            sam.AddCardToHand(twoClubs);
            sam.AddCardToHand(threeClubs);
            Assert.AreEqual(5, sam.PointTotal);
        }

        [Test]
        public void Player_DrawCard_ArgumentNullException()
        {
            var playerOne = new Player(playerName);

            Assert.Throws<ArgumentNullException>(() => playerOne.AddCardToHand(null));
        }

        [Test]
        public void Player_DrawCard_TotalFifteen()
        {
            var playerOne = new Player(playerName);
            playerOne.AddCardToHand(twoClubs);
            playerOne.AddCardToHand(threeClubs);
            playerOne.AddCardToHand(jackClubs);

            Assert.AreEqual(15, playerOne.PointTotal);
        }

        [Test]
        public void Player_DrawCard_FirstCardFaceDown()
        {
            var playerOne = new Player(playerName);
            playerOne.AddCardToHand(twoClubs);

            Assert.AreEqual(true, playerOne.Hand.First().FaceDown);
        }

        [Test]
        public void Player_DrawCard_SecondCardFaceUp()
        {
            var playerOne = new Player(playerName);
            playerOne.AddCardToHand(twoClubs);
            playerOne.AddCardToHand(threeClubs);

            Assert.AreEqual(false, playerOne.Hand[1].FaceDown);
        }

        [Test]
        public void Player_DrawCard_ThirdCardFaceUp()
        {
            var playerOne = new Player(playerName);
            playerOne.AddCardToHand(twoClubs);
            playerOne.AddCardToHand(threeClubs);
            playerOne.AddCardToHand(jackClubs);

            Assert.AreEqual(false, playerOne.Hand[2].FaceDown);
        }
    }
}
