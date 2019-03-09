using System;
using System.Threading.Tasks;
using Entities;

namespace Interactors
{
  public interface IGameRepository
  {
    Task CreateAsync(string identifier, Game game);
    Task<Game> ReadAsync(string identifier);
    Task UpdateAsync(string identifier, Game game);
  }
}
