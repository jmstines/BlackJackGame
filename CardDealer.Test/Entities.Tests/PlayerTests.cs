using CardDealer;
using Tests.Mocks;
using Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
  public class PlayerTests
  {
    [SetUp]
    public void Setup()
    {

    }

    [Test]
    public void NewPlayer_NullName_ArgumentNullException()
    {
      BlackJackCard card = new BlackJackCard();
      List<BlackJackCard> hand = new List<BlackJackCard>();
      hand.Add(card);
      hand.Add(card);
      Player player1 = new Player(null, hand);

	  
    }
  }
}
