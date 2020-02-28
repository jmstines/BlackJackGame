using Entities;
using Entities.Enums;
using Interactors.Boundaries;
using Interactors.Providers;
using Interactors.Repositories;
using System;
using System.Collections.Generic;

namespace Interactors
{
	public class JoinGameInteractor : IInputBoundary<JoinGameInteractor.RequestModel, JoinGameInteractor.ResponseModel>
	{
		public class RequestModel
		{
			public string PlayerId { get; set; }
			public int MaxPlayers { get; set; }
			public int HandCount { get; set; }
		}

		public class ResponseModel
		{
			public string GameIdentifier { get; set; }
		}

		private readonly IGameRepository GameRepository;
		private readonly IGuidBasedIdentifierProviders IdentifierProviders;
		private readonly IPlayerRepository PlayerRepository;
		private readonly IDealerProvider DealerProvider;

		public JoinGameInteractor(
			IGameRepository gameRepository, 
			IPlayerRepository playerRepository, 
			IDealerProvider dealerProvider, 
			IGuidBasedIdentifierProviders identifierProviders
			)
		{
			GameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
			IdentifierProviders = identifierProviders ?? throw new ArgumentNullException(nameof(identifierProviders));
			PlayerRepository = playerRepository ?? throw new ArgumentNullException(nameof(playerRepository));
			DealerProvider = dealerProvider ?? throw new ArgumentNullException(nameof(dealerProvider));
		}

		public void HandleRequestAsync(RequestModel requestModel, IOutputBoundary<ResponseModel> outputBoundary)
		{
			_ = requestModel?.PlayerId ?? throw new ArgumentNullException(nameof(requestModel.PlayerId));

			var player = PlayerRepository.ReadAsync(requestModel.PlayerId);
			var handIds = IdentifierProviders.GenerateHandIds(requestModel.HandCount);
			var currentPlayer = new BlackJackPlayer(player, handIds);
			var keyAndGame = GameRepository.FindByStatusFirstOrDefault(GameStatus.Waiting, requestModel.MaxPlayers);

			string gameIdentifier;
			BlackJackGame game;
			if (string.IsNullOrEmpty(keyAndGame.Key))
			{
				gameIdentifier = IdentifierProviders.GenerateGameId();
				game = new BlackJackGame(DealerProvider.Dealer, requestModel.MaxPlayers);
			}
			else
			{
				gameIdentifier = keyAndGame.Key;
				game = keyAndGame.Value;
			}
			
			game.AddPlayer(currentPlayer);

			GameRepository.UpdateAsync(gameIdentifier, game);

			outputBoundary.HandleResponse(new ResponseModel() { GameIdentifier = gameIdentifier });
		}
	}
}
