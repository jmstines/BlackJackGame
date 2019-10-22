using Entities;
using Interactors.Providers;
using Interactors.Repositories;
using System;
using Interactors.Boundaries;

namespace Interactors
{
	public class BeginGameInteractor : IInputBoundary<BeginGameInteractor.RequestModel, BeginGameInteractor.ResponseModel>
	{
		public class RequestModel
		{
			public string Identifier { get; set; }
			public BlackJackPlayer Dealer { get; set; }
		}

		public class ResponseModel
		{
			public GameStatus Outcome { get; set; }
			public BlackJackGame Game { get; set; }
		}

		private readonly IGameRepository GameRepository;
		private readonly ICardDeckProvider CardDeckProvider;

        public BeginGameInteractor(IGameRepository gameRepository, ICardDeckProvider deckProvider)
        {
            GameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
			CardDeckProvider = deckProvider ?? throw new ArgumentNullException(nameof(deckProvider));
        }

        public async void HandleRequestAsync(RequestModel requestModel, IOutputBoundary<ResponseModel> outputBoundary)
		{
			var game = await GameRepository.ReadAsync(requestModel.Identifier);
			game.AddPlayer(requestModel.Dealer);
			game.DealHands(CardDeckProvider.Deck);
			new BlackJackOutcomes(game).UpdateStatus();

			await GameRepository.UpdateAsync(requestModel.Identifier, game);
			outputBoundary.HandleResponse(new ResponseModel() { Outcome = game.Status, Game = game });
		}
    }
}
