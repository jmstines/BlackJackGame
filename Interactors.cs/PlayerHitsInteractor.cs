using Entities;
using Interactors.Providers;
using Interactors.Repositories;
using System;
using System.Threading.Tasks;

namespace Interactors
{
	class PlayerHitsInteractor
	{
		public class Request
		{
			public string GameIdentifier { get; set; }
			public string PlayerIdentifier { get; set; }
		}

		public class Response
		{
			public GameStatus Outcome { get; set; }
		}

		private readonly IGameRepository GameRepository;
		private readonly ICardDeckProvider CardDeckProvider;

		public PlayerHitsInteractor(IGameRepository gameRepository, ICardDeckProvider deckProvider)
		{
			GameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
			CardDeckProvider = deckProvider ?? throw new ArgumentNullException(nameof(deckProvider));
		}

		public async Task<Response> HandleRequestAsync(Request request) 
		{
		
		}
	}
}
