using Entities.RepositoryDto;
using Microsoft.Azure.Cosmos;
using System.Linq;
using System;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos.Linq;

namespace BlackJackActions.Repositories
{
	class CosmosDbAvitarRepository : IAvitarRepository
	{
		private readonly Container Container;

		public CosmosDbAvitarRepository(Container container)
		{
			Container = container ?? throw new ArgumentNullException(nameof(container));
		}

		public async Task<ItemResponse<AvitarDto>> CreateAsync(AvitarDto player)
		{
			return await Container.CreateItemAsync(player);
		}

		public async Task<FeedResponse<AvitarDto>> ReadAsync(string identifier)
		{
			var feedIterator = Container.GetItemLinqQueryable<AvitarDto>(true)
				.Where(a => a.id == identifier).ToFeedIterator();

			return await feedIterator.ReadNextAsync();
		}

		public void UpdateAsync(string identifier, AvitarDto player)
		{
			throw new NotImplementedException();
		}
	}
}
