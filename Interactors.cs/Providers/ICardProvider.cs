using Entities.Interfaces;
using System.Collections.Generic;

namespace Interactors.Providers
{
	public interface ICardProvider
	{
		IEnumerable<ICard> Cards(int count);
	}
}
