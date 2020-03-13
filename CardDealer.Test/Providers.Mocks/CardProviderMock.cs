using Entities;
using Entities.Enums;
using Entities.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace CardDealer.Tests.Providers.Mocks
{
	class CardProviderMock : ICardProvider
	{
		private readonly Deck cards = new Deck();
		public IEnumerable<ICard> Cards(int count)
		{
			return cards.Take(count); 
		}

		public IEnumerable<ICard> Cards(CardRank rank, CardSuit suit)
		{
			return cards.Where(c => c.Rank == rank && c.Suit == suit);
		}

		public IEnumerable<ICard> Cards(IEnumerable<CardRank> ranks)
		{
			return ranks.SelectMany(r => cards.Where(c => r == c.Rank && c.Suit == CardSuit.Clubs));
		}
	}
}
