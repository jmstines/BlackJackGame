using Entities;
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
	}
}
