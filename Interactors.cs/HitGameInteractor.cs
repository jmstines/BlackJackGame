using Entities;
using Interactors.Repositories;
using System;
using Interactors.Boundaries;
using System.Linq;

namespace Interactors
{
	public class HitGameInteractor : IInputBoundary<HitGameInteractor.RequestModel, HitGameInteractor.ResponseModel>
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

		public HitGameInteractor(IGameRepository gameRepository)
		{
			GameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
		}

		public void HandleRequestAsync(RequestModel requestModel, IOutputBoundary<ResponseModel> outputBoundary)
		{
			var game = GameRepository.ReadAsync(requestModel.Identifier);

			game.PlayerHits();
			if(game.CurrentPlayer.Hand.PointValue > BlackJackConstants.BlackJack)
			{
				game.CurrentPlayer.Hand.IsBust = true;
			}
			
			GameRepository.UpdateAsync(requestModel.Identifier, game);
			outputBoundary.HandleResponse(new ResponseModel() { Game = game });
		}
	}
}
