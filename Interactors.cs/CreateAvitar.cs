using Entities;
using Interactors.Providers;
using Interactors.Repositories;
using System;
using System.Threading.Tasks;

namespace Interactors
{
	public class CreateAvitar
	{
		public class Response
		{
			public string Identifier { get; set; }
		}

		private readonly IPlayerRepository PlayerRepository;
		private readonly IIdentifierProvider IdentifierProvider;
		public CreateAvitar(IPlayerRepository gameRepository, IIdentifierProvider identifierProvider)
		{
			PlayerRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
			IdentifierProvider = identifierProvider ?? throw new ArgumentNullException(nameof(identifierProvider));
		}

		public async Task<Response> HandleRequestAsync(string playerName)
		{
			_ = playerName ?? throw new ArgumentNullException(nameof(playerName));
			var player = new Player(playerName);
			var identifier = IdentifierProvider.Generate();
			await PlayerRepository.CreatePlayerAsync(identifier, player);

			return new Response() { Identifier = identifier };
		}
	}
}
