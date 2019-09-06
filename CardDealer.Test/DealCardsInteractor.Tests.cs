using Interactors.Mocks;
using Interactors.Providers;
using Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Interactors.Tests
{
	public class Tests
  {
    [SetUp]
    public void Setup()
    {

    }

    [Test]
    public void CreateNewDeck_CardCount_52()
    {
      var provider = new CardDeckProvider();
      List<Card> deck = provider.Deck;
      
      Assert.AreEqual(deck.Count, 52);
    }

    [Test]
    public void CreateNewDeck_Suffle_CardsShuffled()
    {
      var random = new RandomProvider((int)DateTime.UtcNow.Ticks);
      CardDeckProvider provider = new CardDeckProvider();
      List<Card> deck = new List<Card>(provider.Deck);
      provider.Shuffle(random);
      List<Card> deckSuffled = provider.Deck;

      Assert.AreNotEqual(deck, deckSuffled);
    }

    [Test]
    public void CreateNewDeck_Suffle_CardsReversed()
    {
      CardDeckProvider provider = new CardDeckProvider();
      List<Card> deck = new List<Card>(provider.Deck);
      var random = new RandomProviderMock(deck.Count);

      provider.Shuffle(random);
      List<Card> deckSuffled = provider.Deck;
      deck.Reverse();
      Assert.AreEqual(deck, deckSuffled);
    }
  }
}