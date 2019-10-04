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
			public Player Dealer { get; set; }
		}

		public class Response
		{
			public GameStatus Outcome { get; set; }
		}

		private readonly IGameRepository GameRepository;
		private readonly ICardDeckProvider CardDeckProvider;

        public BeginGameInteractor(IGameRepository gameRepository, ICardDeckProvider deckProvider)
        {
            GameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
			CardDeckProvider = deckProvider ?? throw new ArgumentNullException(nameof(deckProvider));
        }

        public async Task<Response> HandleRequestAsync(Request request)
        {
			var deck = new DeckShuffler(CardDeckProvider.Deck).Shuffle();
			var game = await GameRepository.ReadAsync(request.Identifier);
			game.AddPlayer(request.Dealer);
			game.DealHands(deck);
			new BlackJackOutcomes(game).UpdateStatus();

			await GameRepository.UpdateAsync(request.Identifier, game);
			return new Response() { Outcome = game.Status };
		}
    }
}
