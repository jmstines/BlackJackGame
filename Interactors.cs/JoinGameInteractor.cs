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
			public string Identifier { get; set; }
        }

		private readonly IGameRepository GameRepository;
		private readonly IIdentifierProvider IdentifierProvider;
		private readonly IPlayerRepository PlayerRepository;

		public JoinGameInteractor(IGameRepository gameRepository, IPlayerRepository playerRepository, IIdentifierProvider identifierProvider)
        {
			GameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
			IdentifierProvider = identifierProvider ?? throw new ArgumentNullException(nameof(identifierProvider));
			PlayerRepository = playerRepository ?? throw new ArgumentNullException(nameof(playerRepository));
        }

		public async void HandleRequestAsync(RequestModel requestModel, IOutputBoundary<ResponseModel> outputBoundary)
		{
			_ = requestModel?.PlayerId ?? throw new ArgumentNullException(nameof(requestModel.PlayerId));

			var player = await PlayerRepository.ReadAsync(requestModel.PlayerId);
			var currentPlayer = new BlackJackPlayer(requestModel.PlayerId, player);

			KeyValuePair<string, BlackJackGame> valuePair = await GameRepository.FindByStatusFirstOrDefault(GameStatus.Waiting);
			var identifier = valuePair.Key ?? IdentifierProvider.Generate();
			var game = valuePair.Value ?? new BlackJackGame();
			game.AddPlayer(currentPlayer);
			if (valuePair.Key == null)
			{
				await GameRepository.CreateAsync(identifier, game);
			}
			else
			{
				await GameRepository.UpdateAsync(identifier, game);
			}

            outputBoundary.HandleResponse(new ResponseModel() { Identifier = identifier });
        }
    }
}
