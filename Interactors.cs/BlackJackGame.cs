using Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Interactors
{
    [Serializable]
    public class BlackJackGame
    {
		public List<Player> Players { get; }
		public List<Card> Deck { get; }
		public Player CurrentPlayer { get; set; }
		public GameStatus Status { get; private set; }

		public BlackJackGame(Player player)
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

		public BlackJackGame(CardGame game)
        {
            Game = game ?? throw new ArgumentNullException(nameof(game));
            BlackJackGameActions.DealHands(game);
            BlackJackOutcomes.CalculateOutcome(game);
        }

        public void PlayerAction(PlayerAction action)
        {
            switch (action)
            {
                case Entities.PlayerAction.Draw:
                    BlackJackGameActions.PlayerDrawsCard(Game);
                    break;
                case Entities.PlayerAction.Hold:
                    BlackJackGameActions.PlayerHolds(Game);
                    break;
                case Entities.PlayerAction.Split:
                    throw new NotImplementedException();
                    //break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(action));
            }
            BlackJackOutcomes.BustHandCheck(Game);
        }
    }
}
