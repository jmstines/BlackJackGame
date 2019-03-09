using System;
using System.Collections.Generic;
using System.Linq;
using Entities;

namespace CardDealer
{
  public class CardDeckProvider
  {
    private readonly List<Suit> Suits = new List<Suit> { Suit.Clubs, Suit.Diamonds, Suit.Hearts, Suit.Spades };
    private readonly List<Tuple<string, string, int>>  Values = new List<Tuple<string, string, int>>{
        new Tuple<string, string, int>("2","2", 2), new Tuple<string, string, int>("3", "3", 3),
        new Tuple<string, string, int>("4", "4", 4), new Tuple<string, string, int>("5", "5", 5),
        new Tuple<string, string, int>("6", "6", 6), new Tuple<string, string, int>("7", "7", 7),
        new Tuple<string, string, int>("8", "8", 8), new Tuple<string, string, int>("9", "9", 9),
        new Tuple<string, string, int>("10", "10", 10), new Tuple<string, string, int>("J", "Jack", 10),
        new Tuple<string, string, int>("Q", "Queen", 10), new Tuple<string, string, int>("K", "King", 10),
        new Tuple<string, string, int>("A", "Ace", 11)};

    public List<Card> Deck { get; private set; }

    public CardDeckProvider() => CreateDeck();

    public CardDeckProvider(List<Tuple<string, string, int>> values)
    {
      Values = values ?? throw new ArgumentNullException(nameof(values));
      Deck = new List<Card>(Suits.Count * Values.Count);
      CreateDeck();
    }

    public CardDeckProvider(List<Card> deck)
    {
      Deck = new List<Card>(deck) ?? throw new ArgumentNullException(nameof(deck));
    }

    public void Shuffle(IRandomProvider random)
    {
      if (random == null) throw new ArgumentNullException(nameof(random));

      var tempDeck = new List<Card>();
      int count = Deck.Count;
      int firstCardIndex = 0;
      while (count > 0)
      {
        int cardIndex = random.Next(firstCardIndex, count);
        Card currentCard = Deck.ElementAt(cardIndex);
        tempDeck.Add(currentCard);
        Deck.Remove(currentCard);
        count--;
      }
      Deck = tempDeck;
    }

    private void CreateDeck()
    {
      Deck = new List<Card>(Suits.Count * Values.Count);
      Suits.ForEach(s => Values.ForEach(v => Deck.Add(new Card(s, v.Item1, v.Item2, v.Item3))));
    }
  }
}
