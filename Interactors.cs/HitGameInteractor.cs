using Entities;
using Interactors.Repositories;
using System;
using Interactors.Boundaries;
using System.Linq;
using Interactors.Providers;

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
		private readonly ICardProviderRandom CardProvider;

		public HitGameInteractor(IGameRepository gameRepository, ICardProviderRandom cardProvider)
		{
			GameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
			CardProvider = cardProvider ?? throw new ArgumentNullException(nameof(cardProvider));
		}

		public void HandleRequestAsync(RequestModel requestModel, IOutputBoundary<ResponseModel> outputBoundary)
		{
			var game = GameRepository.ReadAsync(requestModel.Identifier);
			var card = CardProvider.Cards(1).First();
			game.PlayerHits(card);

			if (game.CurrentPlayer.Equals(game.Dealer))
			{
				new BlackJackOutcomes(game).UpdateStatus();
			}
			
			GameRepository.UpdateAsync(requestModel.Identifier, game);
			outputBoundary.HandleResponse(new ResponseModel() { Game = game });
		}
	}
}
