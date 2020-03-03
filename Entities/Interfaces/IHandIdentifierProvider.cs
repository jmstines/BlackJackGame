using System.Collections.Generic;

namespace Entities.Interfaces
{
	public interface IHandIdentifierProvider
	{
		IEnumerable<string> GenerateHandIds(int count);
	}
}
