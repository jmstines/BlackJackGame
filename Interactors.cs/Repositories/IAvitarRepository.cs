using Entities;
using Entities.RepositoryDto;

namespace Interactors.Repositories
{
	public interface IAvitarRepository
	{
		void CreateAsync(AvitarDto player);
		AvitarDto ReadAsync(string identifier);
		void UpdateAsync(string identifier, AvitarDto player);
	}
}
