using System.Threading.Tasks;
using Entities;

namespace Interactors.Repositories
{
    public interface IGameRepository
    {
        Task CreateGameAsync(string identifier, BlackJackGame game);
        Task<BlackJackGame> ReadAsync(string identifier);
        Task UpdateAsync(string identifier, BlackJackGame game);
        Task<string> AddPlayerToGameAsync(Player player);
    }
}
