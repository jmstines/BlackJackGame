using Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Interactors.Repositories
{
	public interface IPlayerRepository
	{
		Task CreatePlayerAsync(string identifier, Player player);
		Task<Player> ReadAsync(string identifier);
		Task UpdatePlayer(string identifier, Player player);
	}
}
