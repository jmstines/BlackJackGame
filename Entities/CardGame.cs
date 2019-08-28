using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Entities
{
	public class CardGame : ICardGame
	{
		public List<Player> Players { get; }
		public List<Card> Deck { get; }
		public int CurrentPlayerIndex { get; }

		public CardGame(IEnumerable<Card> deck, IEnumerable<string> playerNames, string dealerName)
		{
			Deck = deck.ToList() ?? throw new ArgumentNullException(nameof(deck));
			List<string> names = playerNames.ToList() ?? throw new ArgumentNullException(nameof(playerNames));
			if (names.Count > 4)
			{
				throw new ArgumentOutOfRangeException(nameof(playerNames), "Player Count must be less than 5 Players.");
			}
			else if (names.Count < 1)
			{
				throw new ArgumentOutOfRangeException(nameof(playerNames), "Must have at least one Player.");
			}
			else if (names.Any(p => string.IsNullOrWhiteSpace(p)))
			{
				throw new ArgumentNullException("Players in list must exist.");
			}
			if (string.IsNullOrWhiteSpace(dealerName))
			{
				dealerName = "Dealer";
			}

			Players = new List<Player>();
			names.ForEach(n => Players.Add(new Player(n)));
			CurrentPlayerIndex = 0;
			Players.Add(new Player(dealerName));
		}
	}
}
