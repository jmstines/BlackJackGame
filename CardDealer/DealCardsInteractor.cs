using Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CardDealer
{
  public partial class DealCardsInteractor
  {
    public CardDeckProvider CurrentDeck { get; private set; }
    private readonly List<Card> DefaultDeck = BuildDefualtDeck();

    public DealCardsInteractor() => CurrentDeck = new CardDeckProvider(DefaultDeck);

    private static List<Card> BuildDefualtDeck()
    {
      var deck = new List<Card>();
      var suits = new List<Suit> { Suit.Clubs, Suit.Diamonds, Suit.Hearts, Suit.Spades };
      var values = new List<Tuple<string, string, int>>{
        new Tuple<string, string, int>("2","2", 2), new Tuple<string, string, int>("3", "3", 3),
        new Tuple<string, string, int>("4", "4", 4), new Tuple<string, string, int>("5", "5", 5),
        new Tuple<string, string, int>("6", "6", 6), new Tuple<string, string, int>("7", "7", 7),
        new Tuple<string, string, int>("8", "8", 8), new Tuple<string, string, int>("9", "9", 9),
        new Tuple<string, string, int>("10", "10", 10), new Tuple<string, string, int>("J", "Jack", 10),
        new Tuple<string, string, int>("Q", "Queen", 10), new Tuple<string, string, int>("K", "King", 10),
        new Tuple<string, string, int>("A", "Ace", 11)};

      suits.ForEach(s => values.ForEach(v => deck.Add(new Card(s, v.Item1, v.Item2, v.Item3))));
      return deck;
    }
  }
}
