using Entities;
using Interactors.Providers;
using System;
using System.Threading.Tasks;

namespace Interactors
{
    class BeginGameInteractor
    {
        public class Response
        {
            public string Identifier { get; set; }
        }

        private readonly IGameRepository GameRepository;
        private readonly IIdentifierProvider IdentifierProvider;

        public BeginGameInteractor(IGameRepository gameRepository, IIdentifierProvider identifierProvider)
        {
            GameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
            IdentifierProvider = identifierProvider ?? throw new ArgumentNullException(nameof(identifierProvider));
        }

        public async Task<Response> HandleRequestAsync(string identifier, PlayerAction request)
        {
            //var game = new CardGame();
            //game.ThrowPlayer1(request.Shape);
            //var identifier = IdentifierProvider.Generate();
            //await GameRepository.CreateAsync(identifier, game);
            return new Response() { Identifier = identifier };
        }
    }
}
