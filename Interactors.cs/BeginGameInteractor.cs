using Entities;
using Interactors.Providers;
using Interactors.Repositories;
using System;
using Interactors.Boundaries;
using System.Linq;
using System.Collections.Generic;
using Interactors.ResponceDtos;

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
			public BlackJackGameDto game { get; set; }
		}

		private readonly IGameRepository GameRepository;
		private readonly ICardProviderRandom CardProvider;

        public BeginGameInteractor(IGameRepository gameRepository, ICardProviderRandom cardProvider)
        {
            GameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
			CardProvider = cardProvider ?? throw new ArgumentNullException(nameof(cardProvider));
        }

        public void HandleRequestAsync(RequestModel requestModel, IOutputBoundary<ResponseModel> outputBoundary)
		{
			var game = GameRepository.ReadAsync(requestModel.Identifier);
			game.AddDealer(requestModel.Dealer);
			game.Status = GameStatus.InProgress;
			int twoCardsPerPlayer = game.Players.Count() * 2;
			var cards = CardProvider.Cards(twoCardsPerPlayer);
			foreach (var card in cards)
			{
				game.PlayerHits(card);
				game.PlayerHolds();
			}

			new BlackJackOutcomes(game).UpdateStatus();

			GameRepository.UpdateAsync(requestModel.Identifier, game);
			outputBoundary.HandleResponse(new ResponseModel() { Outcome = game.Status, Game = game });
		}
    }
}
