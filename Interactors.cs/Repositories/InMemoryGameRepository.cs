using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Entities;

namespace Interactors.Repositories
{
	public class InMemoryGameRepository : IGameRepository
	{
		private readonly Dictionary<string, BlackJackGame> Games;

		public InMemoryGameRepository() => Games = new Dictionary<string, BlackJackGame>();

		public async Task CreateAsync(string identifier, BlackJackGame game) =>
			await Task.Run(() => Games.Add(identifier, game));

		public async Task<BlackJackGame> ReadAsync(string identifier) =>
			await Task.Run(() => Games.Single(g => g.Key.Equals(identifier)).Value);

		public async Task UpdateAsync(string identifier, BlackJackGame game) =>
			await Task.Run(() =>
			{
				Games.Remove(identifier);
				Games.Add(identifier, game);
			});

		public async Task<KeyValuePair<string, BlackJackGame>> FindByStatusFirstOrDefault(GameStatus status)
		{
			return await Task.Run(() => Games.FirstOrDefault(g => 
				g.Value.Status == status && 
				g.Value.Players.Count() < BlackJackConstants.MaxPlayerCount)
			);
		}
	}
}
