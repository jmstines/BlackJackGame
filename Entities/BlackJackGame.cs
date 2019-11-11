using System;
using System.Collections.Generic;
using System.Linq;

namespace Entities
{
	public class BlackJackGame
	{
		private readonly List<BlackJackPlayer> players = new List<BlackJackPlayer>();

		public IEnumerable<BlackJackPlayer> Players => players;
		public BlackJackPlayer CurrentPlayer { get; private set; }
		public BlackJackPlayer Dealer { get; private set; }
		public GameStatus Status { get; set; } = GameStatus.Waiting;
		public int MaxPlayerCount { get; private set; }

		public BlackJackGame(int maxPlayers) => MaxPlayerCount = maxPlayers;

		public void AddPlayer(BlackJackPlayer player)
		{
			_ = player ?? throw new ArgumentNullException(nameof(player));
			if (players.Count >= MaxPlayerCount)
			{
				throw new InvalidOperationException($"{player.Name} can NOT join game, The game Status is {Status}.");
			}
			if (Dealer != null)
			{
				throw new InvalidOperationException($"{player.Name} can NOT join game, The Dealer Has Already Joined.");
			}

			if (!Players.Any())
			{
				CurrentPlayer = player;
			}

			players.Add(player);
			SetInProgressOnMaxPlayers();
		}

		public void AddDealer(BlackJackPlayer dealer)
		{
			Dealer = dealer ?? throw new ArgumentNullException(nameof(dealer));
			players.Add(dealer);
			Dealer.Status = PlayerStatusTypes.Ready;
		}

		public void SetPlayerStatusReady(string id)
		{
			players.Where(p => p.PlayerIdentifier.Equals(id)).Single().Status = PlayerStatusTypes.Ready;
			if(players.All(p => p != Dealer && p.Status.Equals(PlayerStatusTypes.Ready)))
			{
				Status = GameStatus.InProgress;
			}
		}

		private void SetInProgressOnMaxPlayers() =>
			Status = players.Count >= MaxPlayerCount ?
			GameStatus.InProgress :
			GameStatus.Waiting;

		public void PlayerHolds() => CurrentPlayer = CurrentPlayer.Equals(players.Last()) ?
			Players.First() :
			Players.ElementAt(players.IndexOf(CurrentPlayer) + 1);

		public void PlayerHits(Card card)
		{
			if (card.Rank == 0 || card.Suit == 0) throw new ArgumentOutOfRangeException(nameof(card));
			CurrentPlayer.Hand.AddCard(new BlackJackCard(card, IsCardFaceDown()));
		}

		private bool IsCardFaceDown() => !CurrentPlayer.Hand.Cards.Any();
	}
}
