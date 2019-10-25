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

		public InMemoryPlayerRepository() => Players = new Dictionary<string, Player>();

		public void CreatePlayerAsync(string identifier, Player player) =>Players.Add(identifier, player);

		public Player ReadAsync(string identifier) => Players.Single(g => g.Key.Equals(identifier)).Value;

		public void UpdatePlayer(string identifier, Player player)
		{
			Players.Remove(identifier);
			Players.Add(identifier, player);
		}
	}
}
