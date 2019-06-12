using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Entities;

namespace CardDealer.Tests.Entities.Mocks
{
	class CardDeckProviderMock : ICardDeckProvider
	{
		public List<Card> Deck => new CardDeckProvider().Deck;

		public List<Card> Deck_DealerWins()
		{
			return new List<Card> {
				Deck.First(c => c.Value.Equals(10)),
				Deck.First(c => c.Value.Equals(10)),
				Deck.First(c => c.Value.Equals(10)),
				Deck.First(c => c.Value.Equals(10)),
				Deck.First(c => c.Value.Equals(10)),
				Deck.First(c => c.Value.Equals(11))
			};
		}

		public List<Card> GetDeck_DealerAndPlayerOneBlackJack()
		{
			return new List<Card> {
				Deck.First(c => c.Value.Equals(10)),
				Deck.First(c => c.Value.Equals(10)),
				Deck.First(c => c.Value.Equals(10)),
				Deck.First(c => c.Value.Equals(11)),
				Deck.First(c => c.Value.Equals(10)),
				Deck.First(c => c.Value.Equals(11))
			};
		}

		public List<Card> Deck_AllPlayersBlackJack()
		{
			return new List<Card> {
				Deck.First(c => c.Value.Equals(10)),
				Deck.First(c => c.Value.Equals(10)),
				Deck.First(c => c.Value.Equals(10)),
				Deck.First(c => c.Value.Equals(11)),
				Deck.First(c => c.Value.Equals(11)),
				Deck.First(c => c.Value.Equals(11))
			};
		}

		public List<Card> Deck_GameOne()
		{
			return new List<Card> {
				Deck.First(c => c.Value.Equals(2)),
				Deck.First(c => c.Value.Equals(2)),
				Deck.First(c => c.Value.Equals(2)),
				Deck.First(c => c.Value.Equals(11)),
				Deck.First(c => c.Value.Equals(11)),
				Deck.First(c => c.Value.Equals(11)),
				Deck.First(c => c.Value.Equals(10)),
				Deck.First(c => c.Value.Equals(2)),
				Deck.First(c => c.Value.Equals(2))
			};
		}

		public List<Card> Deck_GameTwo()
		{
			return new List<Card> {
				Deck.First(c => c.Value.Equals(2)),
				Deck.First(c => c.Value.Equals(2)),
				Deck.First(c => c.Value.Equals(10)),
				Deck.First(c => c.Value.Equals(2)),
				Deck.First(c => c.Value.Equals(10)),
				Deck.First(c => c.Value.Equals(3)),
				Deck.First(c => c.Value.Equals(3)),
				Deck.First(c => c.Value.Equals(10)),
			};
		}
	}
}