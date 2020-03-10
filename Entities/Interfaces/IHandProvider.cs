using System.Collections.Generic;

namespace Entities.Interfaces
{
	public interface IHandProvider
	{
		IEnumerable<Hand> Hands(IEnumerable<string> identifiers);
	}
}
