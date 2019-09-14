using Entities;
using System;
using System.Linq;

namespace Interactors
{
    public static class BlackJackGameActions
    {
        public static void DealHands(CardGame game)
        {
            int twoCardsPerPlayer = game.Players.Count * 2;
            for (int i = 0; i < twoCardsPerPlayer; i++)
            {
                PlayerDrawsCard(game);
                NextPlayer(game);
            }
        }

        public static void PlayerHolds(CardGame game)
        {
            NextPlayer(game);
            if (DealerCurrentPlayer(game))
            {
                DealersFinalTurn(game);
            }
        }

        public static void PlayerDrawsCard(CardGame game)
        {
            Card card = game.Deck.FirstOrDefault() ?? throw new ArgumentOutOfRangeException(nameof(game.Deck), "Card Deck is Empty.");
            game.Deck.Remove(card);
            game.CurrentPlayer.AddCardToHand(new BlackJackCard(card, IsFaceDown(game), GetCardValue(card.Display)));
        }

        private static int GetCardValue(string display)
        {
            if (!int.TryParse(display, out int value))
            {
                value = display.Equals("A") ? BlackJackGameConstants.AceHighValue : BlackJackGameConstants.DefaultCardValue;
            }
            return value;
        }

        private static bool IsFaceDown(CardGame game) => game.CurrentPlayer.Hand.Any() ? false : true;

        private static void DealersFinalTurn(CardGame game)
        {
            while (game.Players.Last().PointTotal < BlackJackGameConstants.DealerHoldValue)
            {
                PlayerDrawsCard(game);
            }
        }

        private static void NextPlayer(CardGame game) => game.CurrentPlayer = DealerCurrentPlayer(game) ?
            game.Players.First() : game.Players.ElementAt(game.Players.IndexOf(game.CurrentPlayer) + 1);

        private static bool DealerCurrentPlayer(CardGame game) => game.CurrentPlayer.Equals(game.Players.Last());
    }
}
