using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Entities
{
	[Serializable]
	public class BlackJackGame
	{
		private const int BlackJack = 21;
		private const int DealerHoldValue = 17;
		public readonly ICardGame CardGame;

		public bool GameComplete { get; private set; }
		public Player CurrentPlayer { get; private set; }
		
		public BlackJackGame(ICardGame game)
		{
			CardGame = game ?? throw new ArgumentNullException(nameof(game));
			CurrentPlayer = CardGame.Players[0];
			GameComplete = false;
			DealHands();
			CalculateOutcome();
		}

		public void PlayerDrawsCard()
		{
			Card card = CardGame.Deck.FirstOrDefault() ?? throw new ArgumentOutOfRangeException(nameof(CardGame.Deck), "Card Deck is Empty.");
			CardGame.Deck.Remove(card);
			CurrentPlayer.AddCardToHand(card);
			if (BustHand(CurrentPlayer))
			{
				PlayerHolds();
			}
		}

		public void PlayerHolds()
		{
			NextPlayer();
			if (CurrentPlayer.Equals(CardGame.Dealer))
			{
				DealersFinalTurn();
			}
		}

		public void NextPlayer()
		{
			if (CurrentPlayer.Equals(CardGame.Dealer))
			{
				CurrentPlayer = CardGame.Players[0];
			}
			else if (CurrentPlayer.Equals(CardGame.Players.Last()))
			{
				CurrentPlayer = CardGame.Dealer;
			}
			else
			{
				CurrentPlayer = CardGame.Players[CardGame.Players.IndexOf(CurrentPlayer) + 1];
			}
		}

		private void DealersFinalTurn()
		{
			while (CardGame.Dealer.PointTotal < DealerHoldValue)
			{
				PlayerDrawsCard();
			}
			CalculateOutcome();
		}

		private void DealHands()
		{
			int twoCardsPerPlayer = (CardGame.Players.Count + 1) * 2;
			for (int i = 0; i < twoCardsPerPlayer; i++)
			{
				PlayerDrawsCard();
				NextPlayer();
			}
		}

		private void CalculateOutcome()
		{
			if (HasBlackjack(CardGame.Dealer))
			{
				CardGame.Players.ForEach(p => p.Status = HasBlackjack(p) ? PlayerStatus.Push : PlayerStatus.PlayerLoses);
			}
			else if (BustHand(CardGame.Dealer))
			{
				CardGame.Players.ForEach(p => p.Status = BustHand(p) ? PlayerStatus.PlayerLoses : PlayerStatus.PlayerWins);
			}
			else
			{
				foreach(Player player in CardGame.Players)
				{
					if (BustHand(player))
					{
						player.Status = PlayerStatus.PlayerLoses;
					}
					else if (PlayerPointsLessThanDealer(player)) {
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
			GameComplete = true;
		}

		private bool HasBlackjack(Player player) => player.PointTotal == BlackJack;
		private bool BustHand(Player player) => player.PointTotal > BlackJack;
		private bool PlayerPointsLessThanDealer(Player player) => player.PointTotal > CardGame.Dealer.PointTotal;
		private bool PlayerPointsEqualsDealer(Player player) => player.PointTotal == CardGame.Dealer.PointTotal;
	}
}
