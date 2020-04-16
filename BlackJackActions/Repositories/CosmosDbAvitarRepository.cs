using Entities.RepositoryDto;
using Interactors.Repositories;
using Microsoft.Azure.Cosmos;
using System;

namespace BlackJackActions.Repositories
{
	class CosmosDbAvitarRepository : IAvitarRepository
	{
		private readonly Container Container;

		public CosmosDbAvitarRepository(Container container)
		{
			Container = container ?? throw new ArgumentNullException(nameof(container));
		}

		public void CreateAsync(AvitarDto player)
		{
			Container.CreateItemAsync(player);
		}

		public AvitarDto ReadAsync(string identifier)
		{
			throw new NotImplementedException();
		}

		public void UpdateAsync(string identifier, AvitarDto player)
		{
			throw new NotImplementedException();
		}
	}
}
