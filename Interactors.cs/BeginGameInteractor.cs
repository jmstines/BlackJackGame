using Entities.Enums;
using Interactors.Boundaries;
using Interactors.Providers;
using Interactors.Repositories;
using Interactors.ResponceDtos;
using Interactors.ResponseDtoMapper;
using System;
using System.Linq;

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
		private readonly ICardProvider CardProvider;
		private readonly IDealerProvicer DealerProvider;

		public BeginGameInteractor(IGameRepository gameRepository, IDealerProvicer dealerProvider, ICardProvider cardProvider)
		{
			GameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
			DealerProvider = dealerProvider ?? throw new ArgumentNullException(nameof(dealerProvider));
			CardProvider = cardProvider ?? throw new ArgumentNullException(nameof(cardProvider));
		}

		public void HandleRequestAsync(RequestModel requestModel, IOutputBoundary<ResponseModel> outputBoundary)
		{
			var game = GameRepository.ReadAsync(requestModel.GameIdentifier);
			game.SetPlayerStatusReady(requestModel.PlayerIdentifier);

			if (game.Players.All(p => p.Status == PlayerStatusTypes.Ready))
			{
				game.AddDealer(DealerProvider.Dealer);
				game.Status = GameStatus.InProgress;

				var cardCount = game.Players.Sum(p => p.Hands.Count());
				var cards = CardProvider.Cards(cardCount);


				game.Players.ToList().ForEach(p => p.Hands.ToList().ForEach(h => h.Value.AddCardRange(cards.Take(2))));
			}
			//new BlackJackOutcomes(game).UpdateStatus();

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