using Entities.Interfaces;
using Interactors.Boundaries;
using Interactors.Repositories;
using Interactors.ResponceDtos;
using Interactors.ResponseDtoMapper;
using System;
using System.Linq;

namespace Interactors
{
	public class HitGameInteractor : IInputBoundary<HitGameInteractor.RequestModel, HitGameInteractor.ResponseModel>
	{
		public class RequestModel
		{
			public string GameIdentifier { get; set; }
			public string PlayerIdentifier { get; set; }
			public string HandIdentifier { get; set; }
		}

		public class ResponseModel
		{
			public BlackJackGameDto Game { get; set; }
		}

		private readonly IGameRepository GameRepository;
		private readonly ICardProvider CardProvider;

		public HitGameInteractor(IGameRepository gameRepository, ICardProvider cardProvider)
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
				game.PlayerHits(requestModel.HandIdentifier, card);

				if (game.CurrentPlayer.Equals(game.Dealer))
				{
					//new BlackJackOutcomes(game).UpdateStatus();
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
