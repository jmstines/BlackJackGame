using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

namespace Interactors.Repositories
{
    public interface IGameRepository
    {
        Task CreateAsync(string identifier, BlackJackGame game);
        Task<BlackJackGame> ReadAsync(string identifier);
        Task UpdateAsync(string identifier, BlackJackGame game);
		Task<KeyValuePair<string, BlackJackGame>> FindByStatusFirstOrDefault(GameStatus status);
    }
}
