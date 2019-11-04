using Entities;
using System.Collections.Generic;

namespace Interactors.Providers
{
	public interface ICardProviderRandom
	{
		IEnumerable<Card> Cards(int count);
	}
}
