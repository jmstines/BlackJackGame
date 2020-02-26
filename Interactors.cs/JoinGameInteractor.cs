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
			public string PlayerIdentifier { get; set; }
		}

		private readonly IGameRepository GameRepository;
		private readonly IGuidBasedIdentifierProviders IdentifierProviders;
		private readonly IPlayerRepository PlayerRepository;

		public JoinGameInteractor(IGameRepository gameRepository, IPlayerRepository playerRepository, IGuidBasedIdentifierProviders identifierProviders)
		{
			GameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
			IdentifierProviders = identifierProviders ?? throw new ArgumentNullException(nameof(identifierProviders));
			PlayerRepository = playerRepository ?? throw new ArgumentNullException(nameof(playerRepository));
		}

		public void HandleRequestAsync(RequestModel requestModel, IOutputBoundary<ResponseModel> outputBoundary)
		{
			_ = requestModel?.PlayerId ?? throw new ArgumentNullException(nameof(requestModel.PlayerId));

			var player = PlayerRepository.ReadAsync(requestModel.PlayerId);
			var playerIdentifier = IdentifierProviders.GeneratePlayerId();
			var handIds = IdentifierProviders.GenerateHandIds(requestModel.HandCount);
			var currentPlayer = new BlackJackPlayer(player, handIds);

			KeyValuePair<string, BlackJackGame> valuePair = GameRepository.FindByStatusFirstOrDefault(GameStatus.Waiting, requestModel.MaxPlayers);
			var gameIdentifier = valuePair.Key ?? IdentifierProviders.GenerateGameId();
			var game = valuePair.Value ?? new BlackJackGame(requestModel.MaxPlayers);
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
