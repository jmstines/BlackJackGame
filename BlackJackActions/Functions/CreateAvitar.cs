using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Entities.RepositoryDto;
using Interactors.Providers;
using Avitar.Repositories;

namespace Avitar
{
	public class CreateAvitar
	{
		private readonly IAvitarRepository Repository;
		private readonly ILogger Logger;
		public CreateAvitar(IAvitarRepository repository, ILogger<CreateAvitar> logger)
		{
			Repository = repository;
			Logger = logger;
		}

		[FunctionName(nameof(CreateAvitar))]
		public async Task<IActionResult> Run(
			[HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "CreateAvitar"
			)] HttpRequest req)
		{
			Logger.LogInformation("C# HTTP trigger function processed a request.");

			string name = req.Query["name"];

			string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
			dynamic data = JsonConvert.DeserializeObject(requestBody);
			name = name ?? data?.name;

			var identifier = new GuidBasedAvitarIdentifierProvider().GenerateAvitar();

			var avitar = new AvitarDto() { id = identifier, userName = name };
			var response = await Repository.SaveAsync(avitar);

			Logger.LogInformation(response.Headers.ToString());

			return name != null
				? (ActionResult)new OkObjectResult($"Hello, {name}: {identifier}")
				: new BadRequestObjectResult("Please pass a name on the query string or in the request body");
		}
	}
}
