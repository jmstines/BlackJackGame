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
			CreateAsync(identifier, game);
		}

		public KeyValuePair<string, BlackJackGame> FindByStatusFirstOrDefault(GameStatus status, int maxPlayers)
		{
			var game = Games.FirstOrDefault(g =>
				g.Value.Status == status &&
				g.Value.Players.Count() < maxPlayers);

			return game.Key == null ? new KeyValuePair<string, BlackJackGame>(string.Empty, null) : game;
		}
	}
}
