using System;
using System.Linq;
using Entities;

namespace Interactors
{
    public static class BlackJackActionDrawCard
    {
        private const int AceLowValue = 1;
        private const int AceHighValue = 11;
        private const int DefaultCardValue = 10;
        private const int BlackJack = 21;

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
                value = display.Equals("A") ? AceHighValue : DefaultCardValue;
            }
            return value;
        }

        private static bool IsFaceDown(CardGame game) => game.CurrentPlayer.Hand.Any() ? false : true;
    }
}
