using Entities;
using System.Collections.Generic;

namespace Interactors.Repositories
{
	public interface IPlayerRepository
	{
		void CreatePlayerAsync(string identifier, BlackJackPlayer player);
		KeyValuePair<string, BlackJackPlayer> ReadAsync(string identifier);
		void UpdatePlayer(string identifier, BlackJackPlayer player);
	}
}
