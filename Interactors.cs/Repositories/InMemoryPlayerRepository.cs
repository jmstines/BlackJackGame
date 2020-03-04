using Entities;
using System.Collections.Generic;
using System.Linq;

namespace Interactors.Repositories
{
	public class InMemoryPlayerRepository : IPlayerRepository
	{
		private readonly Dictionary<string, Avitar> Players;

		public InMemoryPlayerRepository() => Players = new Dictionary<string, Avitar>();

		public void CreatePlayerAsync(string identifier, Avitar player) => Players.Add(identifier, player);

		public KeyValuePair<string, Avitar> ReadAsync(string identifier) => Players.Single(g => g.Key.Equals(identifier));

		public void UpdatePlayer(string identifier, Avitar player)
		{
			Players.Remove(identifier);
			Players.Add(identifier, player);
		}
	}
}
