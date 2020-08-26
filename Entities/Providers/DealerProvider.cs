using Entities.Interfaces;
using Entities.RepositoryDto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Entities.Providers
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
			var ids = new List<string> { 
				AvitarIdentifierProvider.GenerateAvitar(),
				AvitarIdentifierProvider.GenerateAvitar(), 
				AvitarIdentifierProvider.GenerateAvitar(), 
				AvitarIdentifierProvider.GenerateAvitar()
			};

			var dealers = new List<BlackJackPlayer>
			{
				new BlackJackPlayer(new AvitarDto() {
					id = ids[0], userName = "Data" }, HandIdentifierProvider, 1),
				new BlackJackPlayer(new AvitarDto() {
					id = ids[1], userName = "Jerry Maguire" }, HandIdentifierProvider, 1),
				new BlackJackPlayer(new AvitarDto() {
					id = ids[2], userName = "James Bond" }, HandIdentifierProvider, 1),
				new BlackJackPlayer(new AvitarDto() {
					id = ids[3], userName = "Rain Man" }, HandIdentifierProvider, 1)
			};
			return dealers;
		}
	}
}
