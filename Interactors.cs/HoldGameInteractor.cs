using Entities;
using Interactors.Repositories;
using System;
using Interactors.Boundaries;
using System.Linq;

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
			public BlackJackGame Game { get; set; }
		}

		private readonly IGameRepository GameRepository;

		public HoldGameInteractor(IGameRepository gameRepository)
		{
			GameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
		}

		public void HandleRequestAsync(RequestModel requestModel, IOutputBoundary<ResponseModel> outputBoundary)
		{
			var game = GameRepository.ReadAsync(requestModel.Identifier);

			game.PlayerHolds();
			if (game.CurrentPlayer.Equals(game.Dealer))
			{
				new BlackJackOutcomes(game).UpdateStatus();
			}

			GameRepository.UpdateAsync(requestModel.Identifier, game);
			outputBoundary.HandleResponse(new ResponseModel() { Game = game });
		}
	}
}
