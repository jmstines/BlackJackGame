using System;
using Avitar.Helpers;
using Avitar.Repositories;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using Microsoft.Extensions.Logging;

[assembly: FunctionsStartup(typeof(Startup))]

namespace Avitar.Helpers
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton(new CosmosDbRepositorySetup().Container);

            builder.Services.AddSingleton<IAvitarRepository, CosmosDbAvitarRepository>();
            builder.Services.AddLogging();
        }
    }
}