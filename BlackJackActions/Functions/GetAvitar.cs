using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Avitar.Repositories;
using System.Linq;

namespace Avitar
{
    public class GetAvitar
    {
        private readonly IAvitarRepository Repository;
        private readonly ILogger Logger;
        public GetAvitar(IAvitarRepository repository, ILogger<GetAvitar> logger)
        {
            Repository = repository;
            Logger = logger;
        }

        [FunctionName(nameof(GetAvitar))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "GetAvitar")] HttpRequest req)
        {
            Logger.LogInformation("C# HTTP trigger function processed a request.");

            string id = req.Query["id"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            id = id ?? data?.id;

            var response = await Repository.ReadAsync(id);

            Logger.LogInformation(response.Headers.ToString());

            return response != null
                ? (ActionResult)new OkObjectResult($"Hello, {response.Resource.id}: {response.Resource.name}")
                : new BadRequestObjectResult("Please pass a existing id on the query string or in the request body");
        }
    }
}
