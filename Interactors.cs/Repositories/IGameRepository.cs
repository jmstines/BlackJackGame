using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

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
