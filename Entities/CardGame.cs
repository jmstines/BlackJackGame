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
        private string DealerName { get; }

        public CardGame(IEnumerable<Card> deck, string playerName, string dealerName)
        {
            Deck = deck?.ToList() ?? throw new ArgumentNullException(nameof(deck));
            var name = playerName ?? throw new ArgumentNullException(nameof(playerName));

            DealerName = string.IsNullOrWhiteSpace(dealerName) ? "Dealer" : dealerName;

            Status = GameStatus.Waiting;
            Players = new List<Player>();
            Players.Add(new Player(name));
            CurrentPlayer = Players.First();
        }
        public void AddPlayer(string playerName)
        {
            var name = playerName ?? throw new ArgumentNullException(nameof(playerName));
            if (Players.Count >= BlackJackGameConstants.MaxPlayerCount)
            {
                throw new ArgumentOutOfRangeException(nameof(playerName), "Player Count must be less than 5 Players.");
            }
            else if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("Players in list must exist.");
            }
            Players.Add(new Player(name));
            if (Players.Count == BlackJackGameConstants.MaxPlayerCount)
            {
                Status = GameStatus.InProgress;
                Players.Add(new Player(DealerName));
            }
        }
    }
}
