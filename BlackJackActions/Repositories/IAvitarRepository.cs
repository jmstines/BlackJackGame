using Entities.RepositoryDto;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;

namespace Avitar.Repositories
{
	public interface IAvitarRepository
	{
		Task<ItemResponse<AvitarDto>> SaveAsync(AvitarDto player);
		Task<ItemResponse<AvitarDto>> ReadAsync(string identifier);
	}
}
