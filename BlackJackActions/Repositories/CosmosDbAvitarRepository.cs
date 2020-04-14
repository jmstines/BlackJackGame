using Entities;
using Entities.RepositoryDto;
using Interactors.Repositories;
using Microsoft.Azure.Cosmos;
using System;

namespace BlackJackActions.Repositories
{
	class CosmosDbAvitarRepository : IAvitarRepository
	{
		private readonly Container Container;

		public CosmosDbAvitarRepository()
		{
			var cosmosServiceEndpoint = Environment.GetEnvironmentVariable("CosmosDbServiceEndpoint");
			var cosmosAuthKey = Environment.GetEnvironmentVariable("CosmosDbAuthKey");
			var cosmosDatabaseName = Environment.GetEnvironmentVariable("CosmosDbDatabaseName");
			var cosmosContainerName = Environment.GetEnvironmentVariable("CosmosDbContainerName");
			var cosmosPartitionKey = Environment.GetEnvironmentVariable("CosmosDbPartitionKey");

			var client = new CosmosClient(cosmosServiceEndpoint, cosmosAuthKey, new CosmosClientOptions()
			{
				ConnectionMode = ConnectionMode.Direct
			});

			client.CreateDatabaseIfNotExistsAsync(cosmosDatabaseName).Wait();
			var database = client.GetDatabase(cosmosDatabaseName);
			database.CreateContainerIfNotExistsAsync(cosmosContainerName, cosmosPartitionKey);

			Container = database.GetContainer(cosmosContainerName);
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
