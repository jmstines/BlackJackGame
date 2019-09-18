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

        public async Task<Response> JoinGame(string name)
        {
            var game = new CardGame();
            var identifier = IdentifierProvider.Generate();
            await GameRepository.GetOpenGame();
            return new Response() { Identifier = identifier };
        }

        public async Task<Response> HandleRequestAsync(string identifier, PlayerAction request)
        {
            switch (request)
            {
                case Entities.PlayerAction.Draw:
                    BlackJackGameActions.PlayerDrawsCard(Game);
                    break;
                case Entities.PlayerAction.Hold:
                    BlackJackGameActions.PlayerHolds(Game);
                    break;
                case Entities.PlayerAction.Split:
                    throw new NotImplementedException();
                //break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(request));
            }
            BlackJackOutcomes.BustHandCheck(Game);

            //var game = new CardGame();
            //game.ThrowPlayer1(request.Shape);
            //var identifier = IdentifierProvider.Generate();
            await GameRepository.CreateAsync(identifier, Game);
            return new Response() { Identifier = identifier };
        }
    }
}
