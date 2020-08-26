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
using System.Linq;
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

			var avitar = GetAvitarDto(req);
			ItemResponse<AvitarDto> response;

			// TODO This logic is NOT correct.  Need full crud.  right now edit isn't working. 
			if (string.IsNullOrWhiteSpace(avitar.id) == false)
			{
				response = await Repository.ReadAsync(avitar);
			}
			else
			{
				avitar.id ??= new GuidBasedAvitarIdentifierProvider().GenerateAvitar();
				response = await Repository.SaveAsync(avitar);
			}

			Logger.LogInformation(response.Headers.ToString());

			return response?.Resource != null
				? (ActionResult)new OkObjectResult($"Hello, {response.Resource.id}: {response.Resource.userName}")
				: new BadRequestObjectResult("Please pass a name and/or id on the query string or in the request body");
		}

		private AvitarDto GetAvitarDto(HttpRequest request)
		{
			var id = request.Form.ToList().SingleOrDefault(k => k.Key == "id").Value.ToString() ?? string.Empty;
			var userName = request.Form.ToList().SingleOrDefault(k => k.Key == "userName").Value.ToString() ?? string.Empty;
			var email = request.Form.ToList().SingleOrDefault(k => k.Key == "emailAddress").Value.ToString() ?? string.Empty;

			return new AvitarDto() { id = id, userName = userName, emailAddress = email };
		}
	}
}