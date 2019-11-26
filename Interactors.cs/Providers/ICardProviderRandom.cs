using Entities.Interfaces;
using System.Collections.Generic;

namespace Interactors.Providers
{
	public interface ICardProviderRandom
	{
		IEnumerable<ICard> Cards(int count);
	}
}
