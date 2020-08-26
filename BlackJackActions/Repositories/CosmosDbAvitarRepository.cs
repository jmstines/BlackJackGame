using Entities.RepositoryDto;
using Microsoft.Azure.Cosmos;
using System.Linq;
using System;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos.Linq;

namespace Avitar.Repositories
{
	class CosmosDbAvitarRepository : IAvitarRepository
	{
		private readonly Container Container;

		public CosmosDbAvitarRepository(Container container)
		{
			Container = container ?? throw new ArgumentNullException(nameof(container));
		}

		public async Task<ItemResponse<AvitarDto>> SaveAsync(AvitarDto player)
		{
			return await Container.UpsertItemAsync(player);
		}

		public async Task<ItemResponse<AvitarDto>> ReadAsync(AvitarDto player)
		{
			return await Container.ReadItemAsync<AvitarDto>(player.id, new PartitionKey(player.userName));
		}
	}
}
