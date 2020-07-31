using Entities;
using Entities.RepositoryDto;
using System.Collections.Generic;
using System.Linq;

namespace Interactors.Repositories
{
	public class InMemoryAvitarRepository : IAvitarRepository
	{
		private readonly Dictionary<string, AvitarDto> Avitars;

		public InMemoryAvitarRepository()
		{
			Avitars = new Dictionary<string, AvitarDto>();
		}

		public void CreateAsync(AvitarDto player) => Avitars.Add(player.Id, player);

		public AvitarDto ReadAsync(string identifier) => Avitars.Single(g => g.Key.Equals(identifier)).Value;

		public void UpdateAsync(string identifier, AvitarDto player)
		{
			Avitars.Remove(identifier);
			Avitars.Add(identifier, player);
		}
	}
}
