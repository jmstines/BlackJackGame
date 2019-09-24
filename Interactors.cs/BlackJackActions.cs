using Entities;
using System;
using System.Linq;
using Interactors.Providers;
using System.Collections.Generic;

namespace Interactors
{
    public static class BlackJackActions
    {
        public static void DealHands(BlackJackGame game)
        {
            int twoCardsPerPlayer = game.Players.Count * 2;
            for (int i = 0; i < twoCardsPerPlayer; i++)
            {
                PlayerDrawsCard(game.Deck, game.CurrentPlayer.Hand.Cards);	
				NextPlayer(game.Players, game.CurrentPlayer);
            }
        }

        public static void PlayerHolds(BlackJackGame game)
        {
            NextPlayer(game.Players, game.CurrentPlayer);
            if (DealerCurrentPlayer(game.Players, game.CurrentPlayer))
            {
                DealersFinalTurn(game.Deck, game.CurrentPlayer);
            }
        }

        public static void PlayerDrawsCard(List<Card> deck, List<BlackJackCard> hand)
        {
            Card card = deck.FirstOrDefault();
            deck.Remove(card);
            hand.Add(new BlackJackCard(card, IsFaceDown(hand)));      
        }

        private static bool IsFaceDown(List<BlackJackCard> hand) => hand.Any() ? false : true;

        private static void DealersFinalTurn(List<Card> deck, Player player)
        {
            while (player.Hand.PointValue < BlackJackConstants.DealerHoldValue)
            {
                PlayerDrawsCard(deck, player.Hand.Cards);
			}
        }

        private static void NextPlayer(List<Player> players, Player currentPlayer) => 
			currentPlayer = DealerCurrentPlayer(players, currentPlayer) ?
			players.First() : players.ElementAt(players.IndexOf(currentPlayer) + 1);

		private static bool DealerCurrentPlayer(List<Player> players, Player currentPlayer) => currentPlayer.Equals(players.Last());
    }
}
