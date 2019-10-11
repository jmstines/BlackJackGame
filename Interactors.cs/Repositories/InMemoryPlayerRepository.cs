using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities;

namespace Interactors.Repositories
{
	public class InMemoryPlayerRepository : IPlayerRepository
	{
		private readonly Dictionary<string, Player> Players;

		public InMemoryPlayerRepository(Dictionary<string, Player> players) =>
			Players = players ?? throw new ArgumentNullException(nameof(players));

		public async Task CreatePlayerAsync(string identifier, Player player) =>
			await Task.Run(() => Players.Add(identifier, player));

		public async Task<Player> ReadAsync(string identifier) =>
			await Task.Run(() => Players.Single(g => g.Key.Equals(identifier)).Value);

		public async Task UpdatePlayer(string identifier, Player player) =>
			await Task.Run(() =>
			{
				Players.Remove(identifier);
				Players.Add(identifier, player);
			});
	}
}
