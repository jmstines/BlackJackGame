﻿using Entities;
using Entities.Interfaces;
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
				new BlackJackPlayer(new KeyValuePair<string, Avitar> (
					"10111001", new Avitar("Data")), HandIdentifierProvider, 1),
				new BlackJackPlayer(new KeyValuePair<string, Avitar> (
					"SHOW_MONEY", new Avitar("Jerry Maguire")), HandIdentifierProvider, 1),
				new BlackJackPlayer(new KeyValuePair<string, Avitar> (
					"007-SECRET", new Avitar("James Bond")), HandIdentifierProvider, 1),
				new BlackJackPlayer(new KeyValuePair<string, Avitar> (
					"777-7777", new Avitar("Rain Man")), HandIdentifierProvider, 1)
			};
			return dealers;
		}
	}
}
