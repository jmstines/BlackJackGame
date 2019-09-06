using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Entities;

namespace CardDealer
{
	[Serializable]
	public class BlackJackGame
	{
		private const int BlackJack = 21;
		private const int DealerHoldValue = 17;
		private Player CurrentPlayer;
		public readonly CardGame CardGame;
		public bool GameComplete { get; private set; }
		
		public BlackJackGame(CardGame game)
		{
			CardGame = game ?? throw new ArgumentNullException(nameof(game));
			CurrentPlayer = CardGame.Players.ElementAt(CardGame.CurrentPlayerIndex);
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
			if (DealerCurrentPlayer())
			{
				DealersFinalTurn();
			}
		}

		private void NextPlayer() => CurrentPlayer = DealerCurrentPlayer() ? 
			CardGame.Players.First() : CardGame.Players.ElementAt(CardGame.Players.IndexOf(CurrentPlayer) + 1);

		private void DealersFinalTurn()
		{
			while (CardGame.Players.Last().PointTotal < DealerHoldValue)
			{
				PlayerDrawsCard();
			}
			CalculateOutcome();
		}

		private void DealHands()
		{
			int twoCardsPerPlayer = (CardGame.Players.Count) * 2;
			for (int i = 0; i < twoCardsPerPlayer; i++)
			{
				PlayerDrawsCard();
				NextPlayer();
			}
		}

		private void CalculateOutcome()
		{
			if (HasBlackjack(CardGame.Players.Last()))
			{
				CardGame.Players.ForEach(p => p.Status = HasBlackjack(p) ? PlayerStatus.Push : PlayerStatus.PlayerLoses);
			}
			else if (BustHand(CardGame.Players.Last()))
			{
				CardGame.Players.ForEach(p => p.Status = BustHand(p) ? PlayerStatus.PlayerLoses : PlayerStatus.PlayerWins);
			}
			else
			{
				foreach(var player in CardGame.Players.Where(p => !p.Equals(CardGame.Players.Last())))
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
		private bool PlayerPointsLessThanDealer(Player player) => player.PointTotal > CardGame.Players.Last().PointTotal;
		private bool PlayerPointsEqualsDealer(Player player) => player.PointTotal == CardGame.Players.Last().PointTotal;
		private bool DealerCurrentPlayer() => CurrentPlayer.Equals(CardGame.Players.Last());
	}
}
