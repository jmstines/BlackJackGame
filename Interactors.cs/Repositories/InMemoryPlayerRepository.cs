﻿using Entities;
using System.Collections.Generic;
using System.Linq;

namespace Interactors.Repositories
{
	public class InMemoryPlayerRepository : IPlayerRepository
	{
		private readonly Dictionary<string, Player> Players;

		public InMemoryPlayerRepository() => Players = new Dictionary<string, Player>();

		public void CreatePlayerAsync(string identifier, Player player) => Players.Add(identifier, player);

		public KeyValuePair<string, Player> ReadAsync(string identifier) => Players.Single(g => g.Key.Equals(identifier));

		public void UpdatePlayer(string identifier, Player player)
		{
			Players.Remove(identifier);
			Players.Add(identifier, player);
		}
	}
}
