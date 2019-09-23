using Entities;
using Interactors.Providers;
using System;
using System.Threading.Tasks;

namespace Interactors
{
    public class FindGameInteractor
    {
        public class Response
        {
            public string Identifier { get; set; }
        }

        private readonly IGameRepository GameRepository;
        private readonly IIdentifierProvider IdentifierProvider;

        public FindGameInteractor(IGameRepository gameRepository, IIdentifierProvider identifierProvider)
        {
            GameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
            IdentifierProvider = identifierProvider ?? throw new ArgumentNullException(nameof(identifierProvider));
        }

        public async Task<Response> HandleRequestAsync(Player player)
        {
            var identifier = await GameRepository.AddPlayerToGameAsync(player);
            if (identifier == null)
            {
                var deck = new ShuffledDeckProvider(new CardDeckProvider().Deck, new RandomProvider()).Shuffle();
                var game = new CardGame();
                identifier = IdentifierProvider.Generate();
                await GameRepository.CreateAsync(identifier, game);
            }
            return new Response() { Identifier = identifier };
        }
    }
}
