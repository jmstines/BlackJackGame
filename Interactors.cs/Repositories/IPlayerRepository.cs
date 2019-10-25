using Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Interactors.Repositories
{
	public interface IPlayerRepository
	{
		void CreatePlayerAsync(string identifier, Player player);
		Player ReadAsync(string identifier);
		void UpdatePlayer(string identifier, Player player);
	}
}
