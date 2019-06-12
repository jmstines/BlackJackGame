using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Entities
{
	[Serializable]
	public class Game
	{
		private List<Card> Deck { get; set; }

		private const int BlackJack = 21;
		private const int DealerHoldValue = 17;

		public List<Player> Players { get; private set; }
		public Player Dealer { get; private set; }
		public bool GameComplete { get; private set; }
		public Player CurrentPlayer { get; private set; }

		public Game(List<Card> deck, List<Player> players)
		{
			Deck = deck ?? throw new ArgumentNullException(nameof(deck));
			Players = players ?? throw new ArgumentNullException(nameof(players));
			if (Players.Count > 4)
			{
				throw new ArgumentOutOfRangeException(nameof(players), "Player Count must be less than 5 Players.");
			}
			else if (Players.Count < 1)
			{
				throw new ArgumentOutOfRangeException(nameof(players), "Must have at least one Player.");
			}
			else if (Players.Any(p => p.Equals(null)))
			{
				throw new ArgumentNullException("Players in list must exist.");
			}

			Dealer = new Player("Dealer");
			CurrentPlayer = Players[0];
			GameComplete = false;
			DealHands();
			CalculateOutcome();
		}

		public void PlayerDrawsCard()
		{
			Card card = Deck.FirstOrDefault() ?? throw new ArgumentOutOfRangeException(nameof(Deck), "Card Deck is Empty.");
			Deck.Remove(card);
			CurrentPlayer.DrawCard(card);
			UpdatePlayerStatus();
		}

		public void PlayerHolds()
		{
			CurrentPlayer.Status = PlayerStatus.Hold;
			NextPlayer();
		}

		public void NextPlayer()
		{
			if (CurrentPlayer.Equals(Dealer))
			{
				CurrentPlayer = Players[0];
			}
			else if (CurrentPlayer.Equals(Players.Last()))
			{
				CurrentPlayer = Dealer;
				if(CurrentPlayer.Hand.Count() == 2)
				{
					DealersFinalTurn();
				}
			}
			else CurrentPlayer = Players[Players.IndexOf(CurrentPlayer) + 1];
		}

		private void DealersFinalTurn()
		{
			while (Dealer.PointTotal < DealerHoldValue)
			{
				PlayerDrawsCard();
			}
			Dealer.Status = PlayerStatus.Hold;
			CalculateOutcome();
		}

		private void DealHands()
		{
			int twoCardsPerPlayer = (Players.Count + 1) * 2;
			for (int i = 0; i < twoCardsPerPlayer; i++)
			{
				PlayerDrawsCard();
				NextPlayer();
			}
		}

		private void UpdatePlayerStatus()
		{
			if (BustHand(CurrentPlayer))
			{
				PlayerHolds();
			}
			else
			{
				CurrentPlayer.Status = PlayerStatus.InProgress;
			}
		}

		private void CalculateOutcome()
		{
			if (HasBlackjack(Dealer))
			{
				Players.ForEach(p => p.Status = HasBlackjack(p) ? PlayerStatus.Push : PlayerStatus.PlayerLoses);
				GameComplete = true;
			}
			else if (BustHand(Dealer))
			{
				Players.ForEach(p => p.Status = BustHand(p) ? PlayerStatus.PlayerLoses : PlayerStatus.PlayerWins);
				GameComplete = true;
			}
			else
			{
				foreach(Player player in Players)
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
				GameComplete = true;
			}
			//if (!Player.CurentHand.Any() || !Dealer.CurentHand.Any())
			//{
			//  //Outcome = Outcome.Undecided;
			//  //return;
			//}
			//throw new NotImplementedException();
			//if (Player.Value == Dealer.Value)
			//{
			//  Outcome = Outcome.Tie;
			//  return;
			//}
			//if (LeftBeatsRight(Player.Value, Dealer.Value))
			//  Outcome = Outcome.Player1Wins;
			//else
			//  Outcome = Outcome.Player2Wins;
		}

		private bool HasBlackjack(Player player) => player.PointTotal == BlackJack;
		private bool BustHand(Player player) => player.PointTotal > BlackJack;
		private bool PlayerPointsLessThanDealer(Player player) => player.PointTotal > Dealer.PointTotal;
		private bool PlayerPointsEqualsDealer(Player player) => player.PointTotal == Dealer.PointTotal;
	}
}
