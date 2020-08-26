using Entities;
using Entities.Interfaces;
using Entities.RepositoryDto;
using Interactors.Boundaries;
using Interactors.Repositories;
using System;

namespace Interactors
{
	public class CreateAvitar : IInputBoundary<CreateAvitar.RequestModel, CreateAvitar.ResponseModel>
	{
		public class RequestModel
		{
			public string PlayerName { get; set; }
		}
		public class ResponseModel
		{
			public string AvitarIdentifier { get; set; }
		}

		private readonly IAvitarRepository AvitarRepository;
		private readonly IAvitarIdentifierProvider IdentifierProvider;
		public CreateAvitar(IAvitarRepository playerRepository, IAvitarIdentifierProvider identifierProvider)
		{
			AvitarRepository = playerRepository ?? throw new ArgumentNullException(nameof(playerRepository));
			IdentifierProvider = identifierProvider ?? throw new ArgumentNullException(nameof(identifierProvider));
		}

		public void HandleRequestAsync(RequestModel requestModel, IOutputBoundary<ResponseModel> outputBoundary)
		{
			_ = requestModel.PlayerName ?? throw new ArgumentNullException(nameof(requestModel.PlayerName));
			var identifier = IdentifierProvider.GenerateAvitar();
			var player = new AvitarDto(){ id = identifier, userName = requestModel.PlayerName };
			
			AvitarRepository.CreateAsync(player);

			outputBoundary.HandleResponse(new ResponseModel()
			{
				AvitarIdentifier = identifier
			});
		}
	}
}
