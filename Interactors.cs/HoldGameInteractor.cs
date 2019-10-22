using Entities;
using Interactors.Repositories;
using System;
using Interactors.Boundaries;

namespace Interactors
{
	public class HoldGameInteractor : IInputBoundary<HoldGameInteractor.RequestModel, HoldGameInteractor.ResponseModel>
	{
		public class RequestModel
		{
			public string Identifier { get; set; }
		}

		public class ResponseModel
		{
			public GameStatus Outcome { get; set; }
			public BlackJackPlayer CurrentPlayer { get; set; }
		}

		private readonly IGameRepository GameRepository;

		public HoldGameInteractor(IGameRepository gameRepository)
		{
			GameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
		}

		public async void HandleRequestAsync(RequestModel requestModel, IOutputBoundary<ResponseModel> outputBoundary)
		{
			var game = await GameRepository.ReadAsync(requestModel.Identifier);

			game.PlayerHolds();
			new BlackJackOutcomes(game).UpdateStatus();

			await GameRepository.UpdateAsync(requestModel.Identifier, game);
			outputBoundary.HandleResponse(new ResponseModel() { Outcome = game.Status, CurrentPlayer = game.CurrentPlayer });
		}
	}
}
