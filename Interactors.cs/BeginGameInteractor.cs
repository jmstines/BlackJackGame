using Entities;
using Interactors.Providers;
using Interactors.Repositories;
using System;
using Interactors.Boundaries;
using System.Linq;
using Interactors.ResponceDtos;
using Interactors.ResponseDtoMapper;

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
		private readonly ICardProviderRandom CardProvider;
		private readonly IDealerProvicer DealerProvider;

        public BeginGameInteractor(IGameRepository gameRepository, IDealerProvicer dealerProvider, ICardProviderRandom cardProvider)
        {
            GameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
			DealerProvider = dealerProvider ?? throw new ArgumentNullException(nameof(dealerProvider));
			CardProvider = cardProvider ?? throw new ArgumentNullException(nameof(cardProvider));
        }

        public void HandleRequestAsync(RequestModel requestModel, IOutputBoundary<ResponseModel> outputBoundary)
		{
			var game = GameRepository.ReadAsync(requestModel.GameIdentifier);
			game.SetPlayerStatusReady(requestModel.PlayerIdentifier);
			if (game.Status.Equals(GameStatus.InProgress))
			{
				game.AddDealer(DealerProvider.Dealer);

				game.Status = GameStatus.InProgress;
				int twoCardsPerPlayer = game.Players.Count() * 2;
				var cards = CardProvider.Cards(twoCardsPerPlayer);
				foreach (var card in cards)
				{
					game.PlayerHits(card);
					game.PlayerHolds();
				}
				new BlackJackOutcomes(game).UpdateStatus();
			}
			GameRepository.UpdateAsync(requestModel.GameIdentifier, game);
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
