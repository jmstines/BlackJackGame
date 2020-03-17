using Entities;
using System.Collections.Generic;

namespace Interactors.Repositories
{
	public interface IAvitarRepository
	{
		void CreateAsync(string identifier, Avitar player);
		KeyValuePair<string, Avitar> ReadAsync(string identifier);
		void UpdateAsync(string identifier, Avitar player);
	}
}
