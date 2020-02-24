using System.Collections.Generic;

namespace Interactors.Providers
{
	public interface IHandIdentifierProvider
	{
		IEnumerable<string> GenerateHandIds(int count);
	}
}
