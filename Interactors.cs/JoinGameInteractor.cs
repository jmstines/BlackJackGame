using Entities;
using Interactors.Providers;
using Interactors.Repositories;
using System;
using System.Collections.Generic;
using Interactors.Boundaries;

namespace Interactors
{
    public class JoinGameInteractor: IInputBoundary<JoinGameInteractor.RequestModel, JoinGameInteractor.ResponseModel>
    {
		public class RequestModel
		{
			public string PlayerId;
		}
		
		public class ResponseModel
        {
			public string GameIdentifier { get; set; }
			public string PlayerIdentifier { get; set; }
        }

		private readonly IGameRepository GameRepository;
		private readonly IGameIdentifierProvider GameIdentifierProvider;
		private readonly IPlayerIdentifierProvider PlayerIdentifierProvider;
		private readonly IPlayerRepository PlayerRepository;

		public JoinGameInteractor(IGameRepository gameRepository, IPlayerRepository playerRepository, IGameIdentifierProvider gameIdentifierProvider, IPlayerIdentifierProvider playerIdentifierProvider)
        {
			GameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
			GameIdentifierProvider = gameIdentifierProvider ?? throw new ArgumentNullException(nameof(gameIdentifierProvider));
			PlayerRepository = playerRepository ?? throw new ArgumentNullException(nameof(playerRepository));
			PlayerIdentifierProvider = playerIdentifierProvider ?? throw new ArgumentNullException(nameof(playerIdentifierProvider));
		}

		public void HandleRequestAsync(RequestModel requestModel, IOutputBoundary<ResponseModel> outputBoundary)
		{
			_ = requestModel?.PlayerId ?? throw new ArgumentNullException(nameof(requestModel.PlayerId));

			var player = PlayerRepository.ReadAsync(requestModel.PlayerId);
			var playerIdentifier = PlayerIdentifierProvider.Generate();
			var currentPlayer = new BlackJackPlayer(playerIdentifier, player);

			KeyValuePair<string, BlackJackGame> valuePair = GameRepository.FindByStatusFirstOrDefault(GameStatus.Waiting);
			var gameIdentifier = valuePair.Key ?? GameIdentifierProvider.Generate();
			var game = valuePair.Value ?? new BlackJackGame();
			game.AddPlayer(currentPlayer);
			if (valuePair.Key == null)
			{
				GameRepository.CreateAsync(gameIdentifier, game);
			}
			else
			{
				GameRepository.UpdateAsync(gameIdentifier, game);
			}

            outputBoundary.HandleResponse(new ResponseModel() { GameIdentifier = gameIdentifier, PlayerIdentifier = playerIdentifier });
        }
    }
}
