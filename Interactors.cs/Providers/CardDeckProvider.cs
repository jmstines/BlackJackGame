using System;
using System.Collections.Generic;
using System.Linq;
using Entities;

namespace Interactors.Providers
{
  public class CardDeckProvider : ICardDeckProvider
  {
    private readonly List<Suit> Suits = new List<Suit> { Suit.Clubs, Suit.Diamonds, Suit.Hearts, Suit.Spades };
    private readonly List<CardDetail>  Values = new List<CardDetail>{
        new CardDetail("2","2"), new CardDetail("3", "3"),
        new CardDetail("4", "4"), new CardDetail("5", "5"),
        new CardDetail("6", "6"), new CardDetail("7", "7"),
        new CardDetail("8", "8"), new CardDetail("9", "9"),
        new CardDetail("10", "10"), new CardDetail("J", "Jack"),
        new CardDetail("Q", "Queen"), new CardDetail("K", "King"),
        new CardDetail("A", "Ace")};

    public List<Card> Deck { get; private set; }

    public CardDeckProvider() => CreateDeck();

    public CardDeckProvider(List<CardDetail> values)
    {
      Values = values ?? throw new ArgumentNullException(nameof(values));
      Deck = new List<Card>(Suits.Count * Values.Count);
      CreateDeck();
    }

    public CardDeckProvider(List<Card> deck)
    {
      Deck = new List<Card>(deck) ?? throw new ArgumentNullException(nameof(deck));
    }

    private void CreateDeck()
    {
      Deck = new List<Card>(Suits.Count * Values.Count);
      Suits.ForEach(s => Values.ForEach(v => Deck.Add(new Card(s, v.Display, v.Description))));
    }
  }
}
