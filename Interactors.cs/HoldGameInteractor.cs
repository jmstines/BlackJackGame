using Interactors.Boundaries;
using Interactors.Repositories;
using Entities.ResponceDtos;
using System;

namespace Interactors
{
	public class HoldGameInteractor : IInputBoundary<HoldGameInteractor.RequestModel, HoldGameInteractor.ResponseModel>
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

		public HoldGameInteractor(IGameRepository gameRepository)
		{
			GameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
		}

		public void HandleRequestAsync(RequestModel requestModel, IOutputBoundary<ResponseModel> outputBoundary)
		{
			var game = GameRepository.ReadAsync(requestModel.GameIdentifier);

			game.PlayerHolds(requestModel.PlayerIdentifier);

			GameRepository.UpdateAsync(requestModel.GameIdentifier, game);
			var gameDto = new MapperBlackJackGameDto(game);

			outputBoundary.HandleResponse(new ResponseModel() { Game = gameDto.Map(requestModel.PlayerIdentifier) });
		}
	}
}
