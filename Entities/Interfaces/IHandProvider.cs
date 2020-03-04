using System.Collections.Generic;

namespace Entities.Interfaces
{
	public interface IHandProvider
	{
		IDictionary<string, Hand> Hands(IEnumerable<string> identifiers);
	}
}
