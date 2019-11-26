using Entities;

namespace Interactors.Repositories
{
	public interface IPlayerRepository
	{
		void CreatePlayerAsync(string identifier, Player player);
		Player ReadAsync(string identifier);
		void UpdatePlayer(string identifier, Player player);
	}
}
