using System.Collections.Generic;
using System.Linq;
using Interactors.Providers;
using Entities;

namespace Interactors.Mocks
{
    class CardDeckProviderMock : ICardDeckProvider
    {
        public IEnumerable<Card> Deck => new CardDeckProvider().Deck;

        public List<Card> Deck_DealerWins()
        {
            return new List<Card> {
                Deck.First(c => c.Rank.Equals("10")),
                Deck.First(c => c.Rank.Equals("10")),
                Deck.First(c => c.Rank.Equals("10")),
                Deck.First(c => c.Rank.Equals("10")),
                Deck.First(c => c.Rank.Equals("10")),
                Deck.First(c => c.Rank.Equals("A"))
            };
        }

        public List<Card> GetDeck_DealerAndPlayerOneBlackJack()
        {
            return new List<Card> {
                Deck.First(c => c.Rank.Equals("10")),
                Deck.First(c => c.Rank.Equals("10")),
                Deck.First(c => c.Rank.Equals("10")),
                Deck.First(c => c.Rank.Equals("A")),
                Deck.First(c => c.Rank.Equals("10")),
                Deck.First(c => c.Rank.Equals("A"))
            };
        }

        public List<Card> Deck_AllPlayersBlackJack()
        {
            return new List<Card> {
                Deck.First(c => c.Rank.Equals("10")),
                Deck.First(c => c.Rank.Equals("10")),
                Deck.First(c => c.Rank.Equals("10")),
                Deck.First(c => c.Rank.Equals("A")),
                Deck.First(c => c.Rank.Equals("A")),
                Deck.First(c => c.Rank.Equals("A"))
            };
        }

        public IEnumerable<Card> Deck_GameOne()
        {
            return new List<Card> {
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

        public List<Card> Deck_GameTwo()
        {
            return new List<Card> {
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