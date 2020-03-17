using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Interactors.Repositories;
using Entities.Interfaces;
using Entities;
using Interactors.Providers;

namespace BlackJackActions
{
    public sealed class BlackJackActions
    {
        private readonly IAvitarIdentifierProvider IdentifierProvider;

        public BlackJackActions()
        {
            IdentifierProvider = new GuidBasedAvitarIdentifierProvider();
        }

        [FunctionName("CreateAvitar")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            var player = new Avitar(name);
            var identifier = new GuidBasedAvitarIdentifierProvider().GenerateAvitar();
            new InMemoryAvitarRepository().CreateAsync(identifier, player);

            return name != null
                ? (ActionResult)new OkObjectResult($"Hello, {name}: {identifier}")
                : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }
    }
}
