using Entities.RepositoryDto;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;

namespace BlackJackActions.Repositories
{
	public interface IAvitarRepository
	{
		Task<ItemResponse<AvitarDto>> CreateAsync(AvitarDto player);
		Task<FeedResponse<AvitarDto>> ReadAsync(string identifier);
		void UpdateAsync(string identifier, AvitarDto player);
	}
}
