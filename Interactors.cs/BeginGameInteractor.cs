using Entities;
using Interactors.Providers;
using System;
using System.Threading.Tasks;

namespace Interactors
{
    class BeginGameInteractor
    {
		public class Request
		{
			public string Identifier { get; set; }
		}

		public class Response
		{
			public GameStatus Outcome { get; set; }
		}

		private readonly IGameRepository GameRepository;
        private readonly IIdentifierProvider IdentifierProvider;
		private readonly IRandomProvider RandomProvider;
		private readonly ICardDeckProvider CardDeckProvider;

        public BeginGameInteractor(IGameRepository gameRepository, IIdentifierProvider identifierProvider, ICardDeckProvider deckProvider, IRandomProvider random)
        {
            GameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
            IdentifierProvider = identifierProvider ?? throw new ArgumentNullException(nameof(identifierProvider));
			CardDeckProvider = deckProvider ?? throw new ArgumentNullException(nameof(deckProvider));
			RandomProvider = random ?? throw new ArgumentNullException(nameof(random));
        }

        public async Task<Response> HandleRequestAsync(Request request)
        {
			var deck = new ShuffledDeckProvider(CardDeckProvider.Deck, RandomProvider).Shuffle();
			var game = await GameRepository.ReadAsync(request.Identifier);
			game.DealHands(deck);
			new BlackJackOutcomes(game).UpdateStatus();

			await GameRepository.UpdateAsync(request.Identifier, game);
			return new Response() { Outcome = game.Status };
		}
    }
}
