using Entities;
using System.Collections.Generic;
using System.Linq;

namespace Interactors
{
    public static class BlackJackBustHandCheck
    {
        private const int AceLowValue = 1;
        private const int AceHighValue = 11;
        private const int BlackJack = 21;

        public static void BustHandCheck(CardGame game)
        {
            if (BustHand(game.CurrentPlayer))
            {
                if (!TryResetAceValue(game.CurrentPlayer))
                {
                    BlackJackActionHold.PlayerHolds(game);
                }
            }
        }

        private static bool TryResetAceValue(Player player)
        {
            var hasHighValueAce = false;
            BlackJackCard Ace = player.Hand.SingleOrDefault(c => c.Value.Equals(AceHighValue));
            if (Ace != null) 
            {
                Ace.Value = AceLowValue;
                hasHighValueAce = true;
            }
            return hasHighValueAce;
        }

        private static bool BustHand(Player player) => player.PointTotal > BlackJack;
    }
}
