using Entities;
using System.Collections.Generic;
using System.Linq;

namespace Interactors.Repositories
{
	public class InMemoryPlayerRepository : IPlayerRepository
	{
		private readonly Dictionary<string, BlackJackPlayer> Players;

		public InMemoryPlayerRepository() => Players = new Dictionary<string, BlackJackPlayer>();

		public void CreatePlayerAsync(string identifier, BlackJackPlayer player) => Players.Add(identifier, player);

		public KeyValuePair<string, BlackJackPlayer> ReadAsync(string identifier) => Players.Single(g => g.Key.Equals(identifier));

		public void UpdatePlayer(string identifier, BlackJackPlayer player)
		{
			Players.Remove(identifier);
			Players.Add(identifier, player);
		}
	}
}
