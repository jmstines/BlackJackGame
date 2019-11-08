using System;
using System.Collections.Generic;
using Entities;
using System.Linq;

namespace Interactors.Providers
{
	public class DealerProvider : IDealerProvicer
	{
		private readonly IPlayerIdentifierProvider PlayerIdentifierProvider;
		private readonly IEnumerable<BlackJackPlayer> Dealers;

		public BlackJackPlayer Dealer => RandomDealer();

		public DealerProvider(IPlayerIdentifierProvider playerIdentifier)
		{
			PlayerIdentifierProvider = playerIdentifier ?? throw new ArgumentNullException(nameof(playerIdentifier));
			Dealers = DealersList();
		}

		private BlackJackPlayer RandomDealer()
		{
			var Random = new Random((int)DateTime.UtcNow.Ticks);
			var index = Random.Next(minValue: 0, maxValue: Dealers.Count());
			return Dealers.ElementAt(index);
		}

		private IEnumerable<BlackJackPlayer> DealersList()
		{
			var dealers = new List<BlackJackPlayer>
			{
				new BlackJackPlayer(PlayerIdentifierProvider.Generate(), new Player("Data")),
				new BlackJackPlayer(PlayerIdentifierProvider.Generate(), new Player("Jerry Maguire")),
				new BlackJackPlayer(PlayerIdentifierProvider.Generate(), new Player("James Bond")),
				new BlackJackPlayer(PlayerIdentifierProvider.Generate(), new Player("Rain Man"))
			};
			return dealers;
		}
	}
}
