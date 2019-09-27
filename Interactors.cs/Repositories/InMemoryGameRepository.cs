using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Entities;
using Interactors.Providers;

namespace Interactors.Repositories
{
    class InMemoryGameRepository : IGameRepository
    {
        private readonly Dictionary<string, BlackJackGame> Games;
        public InMemoryGameRepository() => 
            Games = new Dictionary<string, BlackJackGame>();

        public async Task CreateAsync(string identifier, BlackJackGame game) => 
            await Task.Run(() => Games.Add(identifier, game));

        public async Task<BlackJackGame> ReadAsync(string identifier) => 
            await Task.Run(() => Games.Single(g => g.Key.Equals(identifier)).Value);

        public async Task UpdateAsync(string identifier, BlackJackGame game)
        {
            await Task.Run(() => { Games.Remove(identifier); Games.Add(identifier, game); });
        }

        public async Task<string> AddPlayerToGameAsync(Player player)
        {
			var valuePair = await Task.Run(() => Games.FirstOrDefault(g => g.Value.Status == GameStatus.InProgress
				&& g.Value.Players.Count() < BlackJackConstants.MaxPlayerCount));
			if(valuePair.Key != null)
			{
				valuePair.Value.AddPlayer(player);
				await Task.Run(() => UpdateAsync(valuePair.Key, valuePair.Value));
			}

			return valuePair.Key ?? string.Empty;
		}
	}
}
