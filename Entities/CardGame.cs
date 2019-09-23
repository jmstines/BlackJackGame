using System;
using System.Collections.Generic;
using System.Linq;

namespace Entities
{
    public class CardGame
    {
        public List<Player> Players { get; }
        public List<Card> Deck { get; }
        public Player CurrentPlayer { get; set; }
        public GameStatus Status { get; private set; }

        public CardGame(Player player)
        {
            _ = player ?? throw new ArgumentNullException(nameof(player));

            Status = GameStatus.Waiting;
            Players = new List<Player>() { player };
			CurrentPlayer = Players.First();
        }

        public void AddPlayer(Player player)
        {
			_ = player ?? throw new ArgumentNullException(nameof(player));
            if (Players.Count >= BlackJackGameConstants.MaxPlayerCount)
            {
                throw new ArgumentOutOfRangeException(nameof(player), "Player Count must be less than 5 Players.");
            }
            Players.Add(player);
			IsGameFull();
		}

		private void IsGameFull()
		{
			if (Players.Count == BlackJackGameConstants.MaxPlayerCount)
			{
				Status = GameStatus.InProgress;
			}
		}
    }
}
