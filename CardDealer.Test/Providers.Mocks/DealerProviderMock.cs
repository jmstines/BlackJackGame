using Entities;
using Entities.Interfaces;
using Entities.RepositoryDto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CardDealer.Tests.Providers.Mocks
{
	class DealerProviderMock : IDealerProvider
	{
		private readonly IEnumerable<BlackJackPlayer> Dealers;
		private readonly IHandIdentifierProvider HandIdentifierProvider;
		private readonly int DealerIndex;

		public BlackJackPlayer Dealer => Dealers.ElementAt(DealerIndex);

		public DealerProviderMock(IHandIdentifierProvider handIdentifierProvider, int dealerIndex)
		{
			HandIdentifierProvider = handIdentifierProvider ?? throw new ArgumentNullException(nameof(handIdentifierProvider));
			if (dealerIndex < 0 || dealerIndex > 3)
			{
				throw new ArgumentOutOfRangeException(nameof(dealerIndex), "Index Must be in range 0-3");
			}
			DealerIndex = dealerIndex;
			Dealers = DealersList();
		}

		private IEnumerable<BlackJackPlayer> DealersList()
		{
			var dealers = new List<BlackJackPlayer>
			{
				new BlackJackPlayer(new AvitarDto() { Id = "10111001", Name = "Data" }, HandIdentifierProvider, 1),
				new BlackJackPlayer(new AvitarDto() { Id = "SHOW_MONEY", Name = "Jerry Maguire" }, HandIdentifierProvider, 1),
				new BlackJackPlayer(new AvitarDto() { Id = "007-SECRET", Name ="James Bond" }, HandIdentifierProvider, 1),
				new BlackJackPlayer(new AvitarDto() { Id = "777-7777", Name = "Rain Man" }, HandIdentifierProvider, 1)
			};
			return dealers;
		}
	}
}
