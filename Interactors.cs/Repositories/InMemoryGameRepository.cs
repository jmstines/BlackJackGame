using Entities;
using Entities.Enums;
using System.Collections.Generic;
using System.Linq;

namespace Interactors.Repositories
{
	public class InMemoryGameRepository : IGameRepository
	{
		private readonly Dictionary<string, BlackJackGame> Games;

		public InMemoryGameRepository() => Games = new Dictionary<string, BlackJackGame>();

		public void CreateAsync(string identifier, BlackJackGame game) => Games.Add(identifier, game);

		public BlackJackGame ReadAsync(string identifier) => Games.Single(g => g.Key.Equals(identifier)).Value;

		public void UpdateAsync(string identifier, BlackJackGame game)
		{
			Games.Remove(identifier);
			Games.Add(identifier, game);
		}

		public KeyValuePair<string, BlackJackGame> FindByStatusFirstOrDefault(GameStatus status, int maxPlayers)
		{
			return Games.FirstOrDefault(g =>
				g.Value.Status == status &&
				g.Value.Players.Count() < maxPlayers);
		}
	}
}
