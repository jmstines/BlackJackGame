using Entities.Interfaces;
using System.Collections.Generic;

namespace Entities.Interfaces
{
	public interface ICardProvider
	{
		IEnumerable<ICard> Cards(int count);
	}
}
