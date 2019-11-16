﻿using System.Collections.Generic;
using System.Linq;
using Interactors.Providers;
using Entities;
using Entities.Interfaces;

namespace Interactors.Mocks
{
    class CardDeckProviderMock : ICardDeckProvider
    {
        public IEnumerable<ICard> Deck => new CardDeckProvider().Deck;

        public List<ICard> Deck_DealerWins()
        {
            return new List<ICard> {
                Deck.First(c => c.Rank.Equals("10")),
                Deck.First(c => c.Rank.Equals("10")),
                Deck.First(c => c.Rank.Equals("10")),
                Deck.First(c => c.Rank.Equals("10")),
                Deck.First(c => c.Rank.Equals("10")),
                Deck.First(c => c.Rank.Equals("A"))
            };
        }

        public List<ICard> GetDeck_DealerAndPlayerOneBlackJack()
        {
            return new List<ICard> {
                Deck.First(c => c.Rank.Equals("10")),
                Deck.First(c => c.Rank.Equals("10")),
                Deck.First(c => c.Rank.Equals("10")),
                Deck.First(c => c.Rank.Equals("A")),
                Deck.First(c => c.Rank.Equals("10")),
                Deck.First(c => c.Rank.Equals("A"))
            };
        }

        public List<ICard> Deck_AllPlayersBlackJack()
        {
            return new List<ICard> {
                Deck.First(c => c.Rank.Equals("10")),
                Deck.First(c => c.Rank.Equals("10")),
                Deck.First(c => c.Rank.Equals("10")),
                Deck.First(c => c.Rank.Equals("A")),
                Deck.First(c => c.Rank.Equals("A")),
                Deck.First(c => c.Rank.Equals("A"))
            };
        }

        public IEnumerable<ICard> Deck_GameOne()
        {
            return new List<ICard> {
                Deck.First(c => c.Rank.Equals("2")),
                Deck.First(c => c.Rank.Equals("2")),
                Deck.First(c => c.Rank.Equals("2")),
                Deck.First(c => c.Rank.Equals("A")),
                Deck.First(c => c.Rank.Equals("A")),
                Deck.First(c => c.Rank.Equals("A")),
                Deck.First(c => c.Rank.Equals("10")),
                Deck.First(c => c.Rank.Equals("3")),
                Deck.First(c => c.Rank.Equals("2"))
            };
        }

        public List<ICard> Deck_GameTwo()
        {
            return new List<ICard> {
                Deck.First(c => c.Rank.Equals("2")),
                Deck.First(c => c.Rank.Equals("2")),
                Deck.First(c => c.Rank.Equals("10")),
                Deck.First(c => c.Rank.Equals("2")),
                Deck.First(c => c.Rank.Equals("10")),
                Deck.First(c => c.Rank.Equals("3")),
                Deck.First(c => c.Rank.Equals("3")),
                Deck.First(c => c.Rank.Equals("10"))
            };
        }
    }
}