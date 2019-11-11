using Entities;
using Interactors.Repositories;
using System;
using Interactors.Boundaries;
using System.Linq;
using Interactors.Providers;
using Interactors.ResponceDtos;
using Interactors.ResponseDtoMapper;

namespace Interactors
{
	public class HitGameInteractor : IInputBoundary<HitGameInteractor.RequestModel, HitGameInteractor.ResponseModel>
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
		private readonly ICardProviderRandom CardProvider;

		public HitGameInteractor(IGameRepository gameRepository, ICardProviderRandom cardProvider)
		{
			GameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
			CardProvider = cardProvider ?? throw new ArgumentNullException(nameof(cardProvider));
		}

		public void HandleRequestAsync(RequestModel requestModel, IOutputBoundary<ResponseModel> outputBoundary)
		{
			var game = GameRepository.ReadAsync(requestModel.GameIdentifier);		
			if (game.CurrentPlayer.PlayerIdentifier.Equals(requestModel.PlayerIdentifier))
			{
				var card = CardProvider.Cards(1).First();
				game.PlayerHits(card);

				if (game.CurrentPlayer.Equals(game.Dealer))
				{
					new BlackJackOutcomes(game).UpdateStatus();
				}
				GameRepository.UpdateAsync(requestModel.GameIdentifier, game);
			}
			var gameDto = new BlackJackGameDtoMapper(game);
			bool showAll = false;
			if (game.CurrentPlayer.Equals(game.Dealer))
			{
				showAll = true;
			}
			outputBoundary.HandleResponse(new ResponseModel() { Game = gameDto.Map(showAll) });
		}
	}
}
