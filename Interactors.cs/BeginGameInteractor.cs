using Interactors.Boundaries;
using Interactors.Repositories;
using Entities.ResponceDto;
using System;
using Entities;

namespace Interactors
{
	public class BeginGameInteractor : IInputBoundary<BeginGameInteractor.RequestModel, BeginGameInteractor.ResponseModel>
	{
		public class RequestModel
		{
			public string GameIdentifier { get; set; }
			public string PlayerIdentifier { get; set; }
		}

		public class ResponseModel
		{
			public BlackJackGameDto Game { get; set; }
		}

		private readonly IGameRepository GameRepository;

		public BeginGameInteractor(IGameRepository gameRepository)
		{
			GameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
		}

		public void HandleRequestAsync(RequestModel requestModel, IOutputBoundary<ResponseModel> outputBoundary)
		{
			var game = GameRepository.ReadAsync(requestModel.GameIdentifier);
			game.SetPlayerStatusReady(requestModel.PlayerIdentifier);
			game.DealHands();

			GameRepository.UpdateAsync(requestModel.GameIdentifier, game);
			var gameDto = MapperBlackJackGameDto.Map(game, requestModel.PlayerIdentifier);
			outputBoundary.HandleResponse(new ResponseModel() { Game = gameDto });
		}
	}
}