using Entities.Interfaces;
using Interactors.Providers;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardDealer.Tests.Providers.Mocks
{
	class CardProviderMock : ICardProvider
	{
		public IEnumerable<ICard> Cards(int count)
		{
			throw new NotImplementedException();
		}
	}
}
