using Entities;
using System;
using System.Collections.Generic;
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
				new BlackJackPlayer(PlayerIdentifierProvider.GeneratePlayerId(), new Player("Data"), new List<string>() { "QWRW-1245" }),
				new BlackJackPlayer(PlayerIdentifierProvider.GeneratePlayerId(), new Player("Jerry Maguire"), new List<string>() { "QWRW-1245" }),
				new BlackJackPlayer(PlayerIdentifierProvider.GeneratePlayerId(), new Player("James Bond"), new List<string>() { "QWRW-1245" }),
				new BlackJackPlayer(PlayerIdentifierProvider.GeneratePlayerId(), new Player("Rain Man"), new List<string>() { "QWRW-1245" })
			};
			return dealers;
		}
	}
}
