using Entities.Interfaces;
using System.Collections.Generic;

namespace Interactors.Providers
{
	public interface ICardDeckProvider
	{
		IEnumerable<ICard> Deck { get; }
	}
}