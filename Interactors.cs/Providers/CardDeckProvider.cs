using System;
using System.Collections.Generic;
using System.Linq;
using Entities;

namespace Interactors.Providers
{
    public class CardDeckProvider : ICardDeckProvider
    {
        private readonly IEnumerable<Suit> Suits = new List<Suit> { Suit.Clubs, Suit.Diamonds, Suit.Hearts, Suit.Spades };
        private readonly IEnumerable<CardDetail> CardDetails = new List<CardDetail>{
            new CardDetail("2","2"), new CardDetail("3", "3"),
            new CardDetail("4", "4"), new CardDetail("5", "5"),
            new CardDetail("6", "6"), new CardDetail("7", "7"),
            new CardDetail("8", "8"), new CardDetail("9", "9"),
            new CardDetail("10", "10"), new CardDetail("J", "Jack"),
            new CardDetail("Q", "Queen"), new CardDetail("K", "King"),
            new CardDetail("A", "Ace")};

        public IEnumerable<Card> Deck { get; private set; }

        public CardDeckProvider()
        {
            Deck = BuildDefualtDeck();
        }

        public CardDeckProvider(IEnumerable<CardDetail> details)
        {
            CardDetails = details ?? throw new ArgumentNullException(nameof(details));
            Deck = BuildDefualtDeck();
        }

        public CardDeckProvider(IEnumerable<Card> deck) => Deck = new List<Card>(deck) ?? throw new ArgumentNullException(nameof(deck));

        private IEnumerable<Card> BuildDefualtDeck() => Suits.SelectMany(s => CardDetails.Select(d => new Card(s, d.Display, d.Description)));
    }
}
