using System;
using System.Collections.Generic;
using System.Linq;

namespace Entities
{
	[Serializable]
	public class BlackJackGame
	{
		private readonly List<BlackJackPlayer> players = new List<BlackJackPlayer>();

		public IEnumerable<BlackJackPlayer> Players => players;
		public BlackJackPlayer CurrentPlayer { get; set; }
		public GameStatus Status { get; set; } = GameStatus.Waiting;

		public BlackJackGame() { }

		public void AddPlayer(BlackJackPlayer player)
		{
			_ = player ?? throw new ArgumentNullException(nameof(player));
			if (players.Count > BlackJackConstants.MaxPlayerCount)
			{
				throw new InvalidOperationException($"{player.Name} can not join game, The game Status is {Status}.");
			}
			if (!Players.Any())
			{
				CurrentPlayer = player;
			}

			players.Add(player);
			SetInProgressOnMaxPlayers();
		}

		private void SetInProgressOnMaxPlayers() =>
			Status = players.Count >= BlackJackConstants.MaxPlayerCount ?
			GameStatus.InProgress :
			GameStatus.Waiting;

		public void PlayerHolds() => CurrentPlayer = GetIsDealerCurrentPlayer() ?
			Players.First() :
			Players.ElementAt(players.IndexOf(CurrentPlayer) + 1);

		public void PlayerHits(Card card)
		{
			if (card.Rank == 0 || card.Suit == 0) throw new ArgumentOutOfRangeException(nameof(card));
			CurrentPlayer.Hand.AddCard(new BlackJackCard(card, IsCardFaceDown()));
		}

		private bool IsCardFaceDown() => !CurrentPlayer.Hand.Cards.Any();

		private bool GetIsDealerCurrentPlayer() => CurrentPlayer.Equals(Players.Last());
	}
}
