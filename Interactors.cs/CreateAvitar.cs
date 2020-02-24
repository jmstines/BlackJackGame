using Entities;
using Interactors.Boundaries;
using Interactors.Providers;
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
			public string Identifier { get; set; }
		}

		private readonly IPlayerRepository PlayerRepository;
		private readonly IAvitarIdentifierProvider IdentifierProvider;
		public CreateAvitar(IPlayerRepository playerRepository, IAvitarIdentifierProvider identifierProvider)
		{
			PlayerRepository = playerRepository ?? throw new ArgumentNullException(nameof(playerRepository));
			IdentifierProvider = identifierProvider ?? throw new ArgumentNullException(nameof(identifierProvider));
		}

		public void HandleRequestAsync(RequestModel requestModel, IOutputBoundary<ResponseModel> outputBoundary)
		{
			_ = requestModel.PlayerName ?? throw new ArgumentNullException(nameof(requestModel.PlayerName));
			var player = new Player(requestModel.PlayerName);
			var identifier = IdentifierProvider.GenerateAvitar();
			PlayerRepository.CreatePlayerAsync(identifier, player);

			outputBoundary.HandleResponse(new ResponseModel()
			{
				Identifier = identifier
			});
		}
	}
}
