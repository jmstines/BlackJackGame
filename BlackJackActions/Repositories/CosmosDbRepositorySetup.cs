using Microsoft.Azure.Cosmos;
using System;

namespace Avitar.Repositories
{
	class CosmosDbRepositorySetup
	{
		public readonly Container Container;
		public CosmosDbRepositorySetup()
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
	}
}
