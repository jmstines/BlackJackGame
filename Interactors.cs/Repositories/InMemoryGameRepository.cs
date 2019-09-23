using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Entities;
using Interactors.Providers;

namespace Interactors.Repositories
{
    class InMemoryGameRepository : IGameRepository
    {
        private readonly Dictionary<string, CardGame> Games;

        public InMemoryGameRepository() => 
            Games = new Dictionary<string, CardGame>();

        public async Task CreateAsync(string identifier, CardGame game) => 
            await Task.Run(() => Games.Add(identifier, game));

        public async Task<CardGame> ReadAsync(string identifier) => 
            await Task.Run(() => Games.Single(g => g.Key.Equals(identifier)).Value);

        public async Task UpdateAsync(string identifier, CardGame game)
        {
            await Task.Run(() => { Games.Remove(identifier); Games.Add(identifier, game); });
        }

        public async Task<string> AddPlayerToGameAsync(Player player)
        {
			var valuePair = await Task.Run(() => Games.FirstOrDefault(g => g.Value.Status == GameStatus.InProgress
				&& g.Value.Players.Count < BlackJackGameConstants.MaxPlayerCount));
			if(valuePair.Key != null)
			{
				valuePair.Value.AddPlayer(player);
				await Task.Run(() => UpdateAsync(valuePair.Key, valuePair.Value));
			}

			return valuePair.Key ?? string.Empty;
		}
	}
}
