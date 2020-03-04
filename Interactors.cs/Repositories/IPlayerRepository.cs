using Entities;
using System.Collections.Generic;

namespace Interactors.Repositories
{
	public interface IPlayerRepository
	{
		void CreatePlayerAsync(string identifier, Avitar player);
		KeyValuePair<string, Avitar> ReadAsync(string identifier);
		void UpdatePlayer(string identifier, Avitar player);
	}
}
