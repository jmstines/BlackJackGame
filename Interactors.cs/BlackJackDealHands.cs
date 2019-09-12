using Entities;
using System.Linq;

namespace Interactors
{
    public static class BlackJackDealHands
    {
        public static void DealHands(CardGame game)
        {
            int twoCardsPerPlayer = game.Players.Count * 2;
            for (int i = 0; i < twoCardsPerPlayer; i++)
            {
                BlackJackActionDrawCard.PlayerDrawsCard(game);
                NextPlayer(game);
            }
        }
        private static void NextPlayer(CardGame game) => game.CurrentPlayer = DealerCurrentPlayer(game) ?
            game.Players.First() : game.Players.ElementAt(game.Players.IndexOf(game.CurrentPlayer) + 1);

        private static bool DealerCurrentPlayer(CardGame game) => game.CurrentPlayer.Equals(game.Players.Last());
    }
}
