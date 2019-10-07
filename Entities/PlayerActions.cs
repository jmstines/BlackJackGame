using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
	public class PlayerAction
	{
		private static List<Card> Deck = new List<Card>();
		private static List<Player> Players { get; set; }
		private static Player CurrentPlayer { get; set; }
		private PlayerAction() { }

		public static Card Hit(List<Card> deck)
		{
			Deck = deck ?? throw new ArgumentNullException(nameof(deck));
			var card = Deck.FirstOrDefault();

			if (card.Rank.Equals(0) || card.Suit.Equals(0)) throw new NullReferenceException(nameof(card));

			deck.Remove(card);
			return card;
		}

		public static Player Hold(List<Player> players, Player player)
		{
			CurrentPlayer = player ?? throw new ArgumentNullException(nameof(player));
			Players = players ?? throw new ArgumentNullException(nameof(players));

			return CurrentPlayerIsDealer() ? Players.First() : Players.ElementAt(Players.IndexOf(CurrentPlayer) + 1);
		}

		private static bool CurrentPlayerIsDealer() => CurrentPlayer.Equals(Players.Last());
	}
}
