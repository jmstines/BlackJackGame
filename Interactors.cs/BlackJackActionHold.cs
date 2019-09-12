using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interactors
{
    public static class BlackJackActionHold
    {
        private const int DealerHoldValue = 17;

        public static void PlayerHolds(CardGame game)
        {
            NextPlayer(game);
            if (DealerCurrentPlayer(game))
            {
                DealersFinalTurn(game);
            }
        }

        private static void DealersFinalTurn(CardGame game)
        {
            while (game.Players.Last().PointTotal < DealerHoldValue)
            {
                BlackJackActionDrawCard.PlayerDrawsCard(game);
            }
            //CalculateOutcome();
        }

        private static void NextPlayer(CardGame game) => game.CurrentPlayer = DealerCurrentPlayer(game) ?
            game.Players.First() : game.Players.ElementAt(game.Players.IndexOf(game.CurrentPlayer) + 1);
        
        private static bool DealerCurrentPlayer(CardGame game) => game.CurrentPlayer.Equals(game.Players.Last());
    }
}
