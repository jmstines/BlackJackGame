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
using Entities.RepositoryDto;
using Entities;
using Interactors.Providers;
using Microsoft.Azure.Cosmos;

namespace BlackJackActions
{
	public class BlackJackActions
	{
		private readonly IAvitarRepository Repository;
		public BlackJackActions(IAvitarRepository repository)
		{
			Repository = repository;
		}

		[FunctionName(nameof(BlackJackActions))]
		public async Task<IActionResult> Run(
			[HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "CreateAvitar"
			)] HttpRequest req,
			ILogger log)
		{
			log.LogInformation("C# HTTP trigger function processed a request.");

			string name = req.Query["name"];

			string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
			dynamic data = JsonConvert.DeserializeObject(requestBody);
			name = name ?? data?.name;

			var identifier = new GuidBasedAvitarIdentifierProvider().GenerateAvitar();

			var avitar = new AvitarDto() { id = identifier, Identifier = identifier, Name = name };
			Repository.CreateAsync(avitar);

			return name != null
				? (ActionResult)new OkObjectResult($"Hello, {name}: {identifier}")
				: new BadRequestObjectResult("Please pass a name on the query string or in the request body");
		}
	}
}
