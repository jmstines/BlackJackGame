using Entities;
using System.Collections.Generic;
using System.Linq;

namespace Interactors.Repositories
{
	public class InMemoryAvitarRepository : IAvitarRepository
	{
		private readonly Dictionary<string, Avitar> Avitars;

		public InMemoryAvitarRepository() => Avitars = new Dictionary<string, Avitar>();

		public void CreateAsync(string identifier, Avitar player) => Avitars.Add(identifier, player);

		public KeyValuePair<string, Avitar> ReadAsync(string identifier) => Avitars.Single(g => g.Key.Equals(identifier));

		public void UpdateAsync(string identifier, Avitar player)
		{
			Avitars.Remove(identifier);
			Avitars.Add(identifier, player);
		}
	}
}
