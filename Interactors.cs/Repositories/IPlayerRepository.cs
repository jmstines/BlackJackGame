using Entities;
using System.Collections.Generic;

namespace Interactors.Repositories
{
	public interface IPlayerRepository
	{
		void CreatePlayerAsync(string identifier, Player player);
		KeyValuePair<string, Player> ReadAsync(string identifier);
		void UpdatePlayer(string identifier, Player player);
	}
}
