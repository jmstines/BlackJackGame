using Entities;
using Interactors.Providers;
using Interactors.Repositories;
using System;
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
			// ?? What happens when a player isn't found??
			var player = await PlayerRepository.ReadAsync(playerIdentifier);
            var identifier = await GameRepository.AddPlayerToGameAsync(player);
            if (identifier == string.Empty)
            {
                var game = new BlackJackGame();
				game.AddPlayer(player);
                identifier = IdentifierProvider.Generate();
                await GameRepository.CreateGameAsync(identifier, game);
            }
            return new Response() { Identifier = identifier };
        }
    }
}
