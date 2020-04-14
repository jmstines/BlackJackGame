using System;
using BlackJackActions.Helpers;
using BlackJackActions.Repositories;
using Interactors.Repositories;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using Microsoft.Extensions.Logging;

[assembly: FunctionsStartup(typeof(Startup))]

namespace BlackJackActions.Helpers
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<IAvitarRepository, CosmosDbAvitarRepository>();
        }

        private Container GetContainer(IServiceProvider options)
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

            return database.GetContainer(cosmosContainerName);
        }
    }
}