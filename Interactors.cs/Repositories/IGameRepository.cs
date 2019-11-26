using Entities;
using Entities.Enums;
using System.Collections.Generic;

namespace Interactors.Repositories
{
	public interface IGameRepository
	{
		void CreateAsync(string identifier, BlackJackGame game);
		BlackJackGame ReadAsync(string identifier);
		void UpdateAsync(string identifier, BlackJackGame game);
		KeyValuePair<string, BlackJackGame> FindByStatusFirstOrDefault(GameStatus status, int maxPlayers);
	}
}
