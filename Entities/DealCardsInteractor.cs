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
      var values = new List<Tuple<string, string>>{
        new Tuple<string, string>("2","2"), new Tuple<string, string>("3", "3"),
        new Tuple<string, string>("4", "4"), new Tuple<string, string>("5", "5"),
        new Tuple<string, string>("6", "6"), new Tuple<string, string>("7", "7"),
        new Tuple<string, string>("8", "8"), new Tuple<string, string>("9", "9"),
        new Tuple<string, string>("10", "10"), new Tuple<string, string>("J", "Jack"),
        new Tuple<string, string>("Q", "Queen"), new Tuple<string, string>("K", "King"),
        new Tuple<string, string>("A", "Ace")};

      suits.ForEach(s => values.ForEach(v => deck.Add(new Card(s, v.Item1, v.Item2))));
      return deck;
    }
  }
}
