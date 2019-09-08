using Entities;
using Interactors.Providers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Interactors
{
    public partial class DealCardsInteractor
    {
        public CardDeckProvider CurrentDeck { get; private set; }
        private readonly List<Card> DefaultDeck = BuildDefualtDeck();
        private static readonly List<Suit> Suits = new List<Suit> { Suit.Clubs, Suit.Diamonds, Suit.Hearts, Suit.Spades };
        private static readonly List<CardDetail> Values = new List<CardDetail>{
                new CardDetail("2","2"), new CardDetail("3", "3"),
                new CardDetail("4", "4"), new CardDetail("5", "5"),
                new CardDetail("6", "6"), new CardDetail("7", "7"),
                new CardDetail("8", "8"), new CardDetail("9", "9"),
                new CardDetail("10", "10"), new CardDetail("J", "Jack"),
                new CardDetail("Q", "Queen"), new CardDetail("K", "King"),
                new CardDetail("A", "Ace")};

        public DealCardsInteractor() => CurrentDeck = new CardDeckProvider(DefaultDeck);

        private static List<Card> BuildDefualtDeck() => Suits.SelectMany(s => Values.Select(v => new Card(s, v.Display, v.Description))).ToList();
    }
}
