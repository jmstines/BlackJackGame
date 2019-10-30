﻿using System.Linq;
using System;
using System.Collections.Generic;

namespace Entities
{
	// TODO - Needs to be set up to run on current player
	//		- too Global
    public class BlackJackOutcomes
    {
		private readonly BlackJackGame Game;
		private readonly BlackJackPlayer Dealer;
		public BlackJackOutcomes(BlackJackGame game)
		{
			Game = game ?? throw new ArgumentNullException(nameof(game));
			Dealer = Game.Players.Last();
		}

		public void UpdateStatus()
        {
            if (HasBlackjack(Dealer))
            {
				DealerBlackJackUpdatePlayers(GetPlayers());
				Game.Status = GameStatus.Complete;
            }
            else if (BustHand(Dealer))
            {
				DealerBustUpdatePlayers(GetPlayers());
				Game.Status = GameStatus.Complete;
			}
            else
            {
                foreach (var player in GetPlayers())
                {
                    if (BustHand(player))
                    {
                        player.Status = PlayerStatus.PlayerLoses;
                    }
                    else if (PlayerPointsLessThanDealer(player))
                    {
                        player.Status = PlayerStatus.PlayerLoses;
                    }
                    else if (PlayerPointsEqualsDealer(player))
                    {
                        player.Status = PlayerStatus.Push;
                    }
                    else
                    {
                        player.Status = PlayerStatus.PlayerWins;
                    }
                }
            }
        }
		private IEnumerable<BlackJackPlayer> GetPlayers() => Game.Players.Where(p => !p.Equals(Game.Players.Last()));
		private void DealerBlackJackUpdatePlayers(IEnumerable<BlackJackPlayer> players) => 
			players.ToList().ForEach(p => p.Status = HasBlackjack(p) ? PlayerStatus.Push : PlayerStatus.PlayerLoses);
		private void DealerBustUpdatePlayers(IEnumerable<BlackJackPlayer> players) => 
			players.ToList().ForEach(p => p.Status = BustHand(p) ? PlayerStatus.PlayerLoses : PlayerStatus.PlayerWins);
		private bool HasBlackjack(BlackJackPlayer player) => player.Hand.PointValue == BlackJackConstants.BlackJack;
        private bool BustHand(BlackJackPlayer player) => player.Hand.PointValue > BlackJackConstants.BlackJack;
        private bool PlayerPointsLessThanDealer(BlackJackPlayer player) => player.Hand.PointValue < Game.Players.Last().Hand.PointValue;
        private bool PlayerPointsEqualsDealer(BlackJackPlayer player) => player.Hand.PointValue == Game.Players.Last().Hand.PointValue;
    }
}
