using Entities;
using System.Collections.Generic;

namespace Interactors.Providers
{
	public interface IHandProvider
	{
		IDictionary<string, Hand> Hands(IEnumerable<string> identifiers);
	}
}
