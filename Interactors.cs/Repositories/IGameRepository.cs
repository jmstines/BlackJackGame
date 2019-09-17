using System.Threading.Tasks;
using Entities;

namespace Interactors
{
    public interface IGameRepository
    {
        Task CreateAsync(string identifier, CardGame game);
        Task<CardGame> ReadAsync(string identifier);
        Task UpdateAsync(string identifier, CardGame game);
    }
}
