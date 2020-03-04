using Entities;
using Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Interactors.Providers
{
	public class DealerProvider : IDealerProvider
	{
		private readonly IAvitarIdentifierProvider AvitarIdentifierProvider;
		private readonly IEnumerable<BlackJackPlayer> Dealers;
		private readonly IHandIdentifierProvider HandIdentifierProvider;

		public BlackJackPlayer Dealer => RandomDealer();

		public DealerProvider(IAvitarIdentifierProvider avitarIdentifier, IHandIdentifierProvider handIdentifierProvider)
		{
			AvitarIdentifierProvider = avitarIdentifier ?? throw new ArgumentNullException(nameof(avitarIdentifier));
			HandIdentifierProvider = handIdentifierProvider ?? throw new ArgumentNullException(nameof(handIdentifierProvider));
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
				new BlackJackPlayer(new KeyValuePair<string, Avitar> (
					AvitarIdentifierProvider.GenerateAvitar(), new Avitar("Data")), HandIdentifierProvider),
				new BlackJackPlayer(new KeyValuePair<string, Avitar> (
					AvitarIdentifierProvider.GenerateAvitar(), new Avitar("Jerry Maguire")), HandIdentifierProvider),
				new BlackJackPlayer(new KeyValuePair<string, Avitar> (
					AvitarIdentifierProvider.GenerateAvitar(), new Avitar("James Bond")), HandIdentifierProvider),
				new BlackJackPlayer(new KeyValuePair<string, Avitar> (
					AvitarIdentifierProvider.GenerateAvitar(), new Avitar("Rain Man")), HandIdentifierProvider)
			};
			return dealers;
		}
	}
}
