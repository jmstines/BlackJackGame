using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents;
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
		//private readonly IAvitarIdentifierProvider IdentifierProvider;

		//public BlackJackActions()
		//{
		//    IdentifierProvider = new GuidBasedAvitarIdentifierProvider();
		//}

		//[FunctionName("CreateAvitar")]
		//public static async Task<IActionResult> Run(
		//	[HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
		//	[CosmosDB(
		//		databaseName: "trinugBlackJack",
		//		collectionName: "AvitarRepository",
		//		ConnectionStringSetting = "CosmosDBConnection")]IAsyncCollector<AvitarDto> avitarDto,
		//	ILogger log)
		//{
		//	log.LogInformation("C# HTTP trigger function processed a request.");

		//	string name = req.Query["name"];
		//	string identifier = string.Empty;


		//	if (!string.IsNullOrEmpty(name))
		//	{
		//		string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
		//		dynamic data = JsonConvert.DeserializeObject(requestBody);
		//		name = name ?? data?.name;

		//		identifier = new GuidBasedAvitarIdentifierProvider().GenerateAvitar();
		//		var avitar = new AvitarDto() { Identifier = identifier, Name = name };

		//		//new InMemoryAvitarRepository().CreateAsync(identifier, player);
		//		await avitarDto.AddAsync(avitar);
		//	}

		//	return name != null
		//		? (ActionResult)new OkObjectResult($"Hello, {name}: {identifier}")
		//		: new BadRequestObjectResult("Please pass a name on the query string or in the request body");
		//}

		[FunctionName(nameof(BlackJackActions))]
		public static async Task<IActionResult> Run(
			[HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "CreateAvitar")] HttpRequest req,
			[CosmosDB(
				databaseName: "blackjack-repositories",
				collectionName: "avitar-repo",
				ConnectionStringSetting = "CosmosDBConnection",
				CreateIfNotExists = false,
				PartitionKey = "/Identifier"
			)] IAsyncCollector<AvitarDto> avitarDto,
			ILogger log)
		{
			log.LogInformation("C# HTTP trigger function processed a request.");

			string name = req.Query["name"];

			string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
			dynamic data = JsonConvert.DeserializeObject(requestBody);
			name = name ?? data?.name;

			var identifier = new GuidBasedAvitarIdentifierProvider().GenerateAvitar();

			var avitar = new AvitarDto() { Identifier = identifier, Name = name };
			await avitarDto.AddAsync(avitar);

			return name != null
				? (ActionResult)new OkObjectResult($"Hello, {name}: {identifier}")
				: new BadRequestObjectResult("Please pass a name on the query string or in the request body");
		}
	}
}
