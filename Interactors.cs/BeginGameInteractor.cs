using Interactors.Providers;
using System;

namespace Interactors
{
    class BeginGameInteractor
    {
        public class Response
        {
            public string Identifier { get; set; }
        }

        private readonly IGameRepository gameRepository;
        private readonly IIdentifierProvider identifierProvider;

        public BeginGameInteractor(IGameRepository gameRepository, IIdentifierProvider identifierProvider)
        {
            gameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
            this.identifierProvider = identifierProvider ?? throw new ArgumentNullException(nameof(identifierProvider));
        }

        //public async Task<Response> HandleRequestAsync(Request request)
        //{
        //  var game = new Game();
        //  game.ThrowPlayer1(request.Shape);
        //  var identifier = identifierProvider.Generate();
        //  await gameRepository.CreateAsync(identifier, game);
        //  return new Response() { Identifier = identifier };
        //}
    }
}
