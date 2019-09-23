using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Interactors.Providers;

namespace Interactors
{
    [Serializable]
    public class BlackJackGame
    {
		public List<Player> Players { get; }
		public List<Card> Deck { get; }
		public Player CurrentPlayer { get; set; }
		public GameStatus Status { get; set; }

		public BlackJackGame(Player player)
		{
			_ = player ?? throw new ArgumentNullException(nameof(player));

			Status = GameStatus.Waiting;
			Players = new List<Player>() { player };
			CurrentPlayer = Players.First();
		}

		public void AddPlayer(Player player)
		{
			_ = player ?? throw new ArgumentNullException(nameof(player));
			if (Players.Count >= BlackJackGameConstants.MaxPlayerCount)
			{
				throw new ArgumentOutOfRangeException(nameof(player), "Player Count must be less than 5 Players.");
			}
			Players.Add(player);
			IsGameFull();
		}

		private void IsGameFull()
		{
			if (Players.Count == BlackJackGameConstants.MaxPlayerCount)
			{
				Status = GameStatus.InProgress;
			}
		}

		public void StartGame()
		{
			DealHands();
		}

		public void DealHands()
		{
			int twoCardsPerPlayer = Players.Count * 2;
			for (int i = 0; i < twoCardsPerPlayer; i++)
			{
				PlayerDrawsCard();
				NextPlayer();
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

		public void PlayerDrawsCard()
		{
			Card card = Deck.FirstOrDefault();
			Deck.Remove(card);
			CurrentPlayer.AddCardToHand(new BlackJackCard(card, IsFaceDown()));
			CurrentPlayer.PointTotal = new HandValueProvider(CurrentPlayer.Hand).Value();
		}

		private bool IsFaceDown() => CurrentPlayer.Hand.Any() ? false : true;

		private void DealersFinalTurn()
		{
			while (Players.Last().PointTotal < BlackJackGameConstants.DealerHoldValue)
			{
				PlayerDrawsCard();
			}
		}

		private void NextPlayer() => CurrentPlayer = DealerCurrentPlayer() ?
			Players.First() : Players.ElementAt(Players.IndexOf(CurrentPlayer) + 1);

		private bool DealerCurrentPlayer() => CurrentPlayer.Equals(Players.Last());

		//public void PlayerAction(PlayerAction action)
		//{
		//    switch (action)
		//    {
		//        case Entities.PlayerAction.Draw:
		//            BlackJackGameActions.PlayerDrawsCard(Game);
		//            break;
		//        case Entities.PlayerAction.Hold:
		//            BlackJackGameActions.PlayerHolds(Game);
		//            break;
		//        case Entities.PlayerAction.Split:
		//            throw new NotImplementedException();
		//            //break;
		//        default:
		//            throw new ArgumentOutOfRangeException(nameof(action));
		//    }
		//    BlackJackOutcomes.BustHandCheck(Game);
		//}
	}
}
