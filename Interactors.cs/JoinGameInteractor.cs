using Entities;
using Interactors.Providers;
using Interactors.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Interactors
{
    public class JoinGameInteractor
    {
		public class Response
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

        public async Task<Response> HandleRequestAsync(string playerIdentifier)
        {
			_ = playerIdentifier ?? throw new ArgumentNullException(nameof(playerIdentifier));

			var player = await PlayerRepository.ReadAsync(playerIdentifier);
			var currentPlayer = new BlackJackPlayer(playerIdentifier, player);

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

            return new Response() { Identifier = identifier };
        }
    }
}
