using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
	public interface ICardGame
	{
		List<Player> Players { get; }
		List<Card> Deck { get; }
		int CurrentPlayerIndex { get; }
	}
}
