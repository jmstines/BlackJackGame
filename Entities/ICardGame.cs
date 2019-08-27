using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
	public interface ICardGame
	{
		Player Dealer { get; }
		List<Player> Players { get; }
		List<Card> Deck { get; }
	}
}
