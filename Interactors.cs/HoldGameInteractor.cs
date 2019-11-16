using Entities;
using Interactors.Repositories;
using System;
using Interactors.Boundaries;
using Interactors.ResponceDtos;
using Interactors.ResponseDtoMapper;

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
			if (game.CurrentPlayer.PlayerIdentifier.Equals(requestModel.PlayerIdentifier))
			{
				game.PlayerHolds();
				if (game.CurrentPlayer.Equals(game.Dealer))
				{
					new BlackJackOutcomes(game).UpdateStatus();
				}

				GameRepository.UpdateAsync(requestModel.GameIdentifier, game);
			}
			var gameDto = new BlackJackGameDtoMapper(game);
			bool showAll = false;
			if(game.CurrentPlayer.Equals(game.Dealer))
			{
				showAll = true;
			}
			outputBoundary.HandleResponse(new ResponseModel() { Game = gameDto.Map(showAll) });
		}
	}
}
