using Entities;
using Interactors.Providers;
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

        public JoinGameInteractor(IGameRepository gameRepository, IIdentifierProvider identifierProvider)
        {
            GameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
            IdentifierProvider = identifierProvider ?? throw new ArgumentNullException(nameof(identifierProvider));
        }

        public async Task<Response> HandleRequestAsync(Player player)
        {
			_ = player ?? throw new ArgumentNullException(nameof(player));
            var identifier = await GameRepository.AddPlayerToGameAsync(player);
            if (identifier == string.Empty)
            {
                var game = new BlackJackGame(player);
                identifier = IdentifierProvider.Generate();
                await GameRepository.CreateAsync(identifier, game);
            }
            return new Response() { Identifier = identifier };
        }
    }
}
