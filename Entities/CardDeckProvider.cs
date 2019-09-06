using System;
using System.Collections.Generic;
using System.Linq;
using Entities;

namespace Entities
{
  public class CardDeckProvider : ICardDeckProvider
  {
    private readonly List<Suit> Suits = new List<Suit> { Suit.Clubs, Suit.Diamonds, Suit.Hearts, Suit.Spades };
    private readonly List<Tuple<string, string>>  Values = new List<Tuple<string, string>>{
        new Tuple<string, string>("2","2"), new Tuple<string, string>("3", "3"),
        new Tuple<string, string>("4", "4"), new Tuple<string, string>("5", "5"),
        new Tuple<string, string>("6", "6"), new Tuple<string, string>("7", "7"),
        new Tuple<string, string>("8", "8"), new Tuple<string, string>("9", "9"),
        new Tuple<string, string>("10", "10"), new Tuple<string, string>("J", "Jack"),
        new Tuple<string, string>("Q", "Queen"), new Tuple<string, string>("K", "King"),
        new Tuple<string, string>("A", "Ace")};

    public List<Card> Deck { get; private set; }

    public CardDeckProvider() => CreateDeck();

    public CardDeckProvider(List<Tuple<string, string>> values)
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
      Suits.ForEach(s => Values.ForEach(v => Deck.Add(new Card(s, v.Item1, v.Item2))));
    }
  }
}
