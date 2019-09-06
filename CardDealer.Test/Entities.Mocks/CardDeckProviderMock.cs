using System.Collections.Generic;
using System.Linq;

namespace Entities.Mocks
{
	class CardDeckProviderMock : ICardDeckProvider
	{
		public List<Card> Deck => new CardDeckProvider().Deck;

		public List<Card> Deck_DealerWins()
		{
			return new List<Card> {
				Deck.First(c => c.Display.Equals("10")),
				Deck.First(c => c.Display.Equals("10")),
				Deck.First(c => c.Display.Equals("10")),
				Deck.First(c => c.Display.Equals("10")),
				Deck.First(c => c.Display.Equals("10")),
				Deck.First(c => c.Display.Equals("A"))
			};
		}

		public List<Card> GetDeck_DealerAndPlayerOneBlackJack()
		{
			return new List<Card> {
				Deck.First(c => c.Display.Equals("10")),
				Deck.First(c => c.Display.Equals("10")),
				Deck.First(c => c.Display.Equals("10")),
				Deck.First(c => c.Display.Equals("A")),
				Deck.First(c => c.Display.Equals("10")),
				Deck.First(c => c.Display.Equals("A"))
			};
		}

		public List<Card> Deck_AllPlayersBlackJack()
		{
			return new List<Card> {
				Deck.First(c => c.Display.Equals("10")),
				Deck.First(c => c.Display.Equals("10")),
				Deck.First(c => c.Display.Equals("10")),
				Deck.First(c => c.Display.Equals("A")),
				Deck.First(c => c.Display.Equals("A")),
				Deck.First(c => c.Display.Equals("A"))
			};
		}

		public List<Card> Deck_GameOne()
		{
			return new List<Card> {
				Deck.First(c => c.Display.Equals("2")),
				Deck.First(c => c.Display.Equals("2")),
				Deck.First(c => c.Display.Equals("2")),
				Deck.First(c => c.Display.Equals("A")),
				Deck.First(c => c.Display.Equals("A")),
				Deck.First(c => c.Display.Equals("A")),
				Deck.First(c => c.Display.Equals("10")),
				Deck.First(c => c.Display.Equals("3")),
				Deck.First(c => c.Display.Equals("2"))
			};
		}

		public List<Card> Deck_GameTwo()
		{
			return new List<Card> {
				Deck.First(c => c.Display.Equals("2")),
				Deck.First(c => c.Display.Equals("2")),
				Deck.First(c => c.Display.Equals("10")),
				Deck.First(c => c.Display.Equals("2")),
				Deck.First(c => c.Display.Equals("10")),
				Deck.First(c => c.Display.Equals("3")),
				Deck.First(c => c.Display.Equals("3")),
				Deck.First(c => c.Display.Equals("10"))
			};
		}
	}
}