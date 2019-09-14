using Entities;
using System.Linq;

namespace Interactors
{
    public static class BlackJackOutcomes
    {
        public static void CalculateOutcome(CardGame game)
        {
            if (HasBlackjack(game.Players.Last()))
            {
                game.Players.ForEach(p => p.Status = HasBlackjack(p) ? PlayerStatus.Push : PlayerStatus.PlayerLoses);
            }
            else if (BustHand(game.Players.Last()))
            {
                game.Players.ForEach(p => p.Status = BustHand(p) ? PlayerStatus.PlayerLoses : PlayerStatus.PlayerWins);
            }
            else
            {
                foreach (var player in game.Players.Where(p => !p.Equals(game.Players.Last())))
                {
                    if (BustHand(player))
                    {
                        player.Status = PlayerStatus.PlayerLoses;
                    }
                    else if (PlayerPointsLessThanDealer(game))
                    {
                        player.Status = PlayerStatus.PlayerLoses;
                    }
                    else if (PlayerPointsEqualsDealer(game))
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

        public static void BustHandCheck(CardGame game)
        {
            if (BustHand(game.CurrentPlayer))
            {
                if (!TryResetAceValue(game.CurrentPlayer))
                {
                    BlackJackGameActions.PlayerHolds(game);
                }
            }
        }

        private static bool TryResetAceValue(Player player)
        {
            var hasHighValueAce = false;
            BlackJackCard Ace = player.Hand.SingleOrDefault(c => c.Value.Equals(BlackJackGameConstants.AceHighValue));
            if (Ace != null)
            {
                Ace.Value = BlackJackGameConstants.AceLowValue;
                hasHighValueAce = true;
            }
            return hasHighValueAce;
        }

        private static bool HasBlackjack(Player player) => player.PointTotal == BlackJackGameConstants.BlackJack;
        private static bool BustHand(Player player) => player.PointTotal > BlackJackGameConstants.BlackJack;
        private static bool PlayerPointsLessThanDealer(CardGame game) => game.CurrentPlayer.PointTotal > game.Players.Last().PointTotal;
        private static bool PlayerPointsEqualsDealer(CardGame game) => game.CurrentPlayer.PointTotal == game.Players.Last().PointTotal;
    }
}
