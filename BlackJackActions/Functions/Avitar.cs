using Avitar.Repositories;
using Entities.RepositoryDto;
using Interactors.Providers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace Avitar
{
	public class Avitar
	{
		private readonly IAvitarRepository Repository;
		private readonly ILogger Logger;
		public Avitar(IAvitarRepository repository, ILogger<Avitar> logger)
		{
			Repository = repository;
			Logger = logger;
		}

		[FunctionName(nameof(Avitar))]
		public async Task<IActionResult> Run(
			[HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "Avitar"
			)] HttpRequest req)
		{
			Logger.LogInformation("C# HTTP trigger function processed a request.");
			var identifier = await GetIdFromUrlOrBody(req);
			var name = await GetNameFromUrlOrBody(req);
			ItemResponse<AvitarDto> response;

			if (IsAvitarGetRequest(identifier, name))
			{
				// TODO Test to make sure the read still works after migrating from the linq method.
				response = await Repository.ReadAsync(identifier);
			}
			else
			{
				// TODO This works but I need to validate the update not deleting existing record is exceptable.
				identifier ??= new GuidBasedAvitarIdentifierProvider().GenerateAvitar();
				var avitar = new AvitarDto() { id = identifier, name = name };
				response = await Repository.SaveAsync(avitar);
			}

			Logger.LogInformation(response.Headers.ToString());

			return response?.Resource != null
				? (ActionResult)new OkObjectResult($"Hello, {response.Resource.id}: {response.Resource.name}")
				: new BadRequestObjectResult("Please pass a name and/or id on the query string or in the request body");
		}

		private bool IsAvitarGetRequest(string id, string name)
		{
			return string.IsNullOrWhiteSpace(name) && string.IsNullOrWhiteSpace(id) == false;
		} 

		private async Task<string> GetIdFromUrlOrBody(HttpRequest request)
		{
			string id = request.Query["id"];

			string requestBody = await new StreamReader(request.Body).ReadToEndAsync();
			dynamic data = JsonConvert.DeserializeObject(requestBody);
			id ??= data?.id;

			return id;
		}

		private async Task<string> GetNameFromUrlOrBody(HttpRequest request)
		{
			string name = request.Query["name"];

			string requestBody = await new StreamReader(request.Body).ReadToEndAsync();
			dynamic data = JsonConvert.DeserializeObject(requestBody);
			name ??= data?.name;

			return name;
		}

		//private async Task<string> GetParameter(HttpRequest request, string parameter)
		//{
		//	string output = request.Query[parameter];

		//	string requestBody = await new StreamReader(request.Body).ReadToEndAsync();
		//	dynamic data = JsonConvert.DeserializeObject(requestBody);
		//	output = output ?? data?.parameter1;

		//	return name;
		//}
	}
}