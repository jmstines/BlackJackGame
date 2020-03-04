using System.Collections.Generic;

namespace Entities.Interfaces
{
	public interface ICardDeckProvider
	{
		IEnumerable<ICard> Deck { get; }
	}
}