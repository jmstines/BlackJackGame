using Entities.Enums;
using Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Entities
{
	public class BlackJackGame
	{
		private readonly List<BlackJackPlayer> players = new List<BlackJackPlayer>();
		private readonly ICardProvider cardProvider;

		public IEnumerable<BlackJackPlayer> Players => players;
		public BlackJackPlayer CurrentPlayer { get; private set; }
		public BlackJackPlayer Dealer { get; private set; }
		public GameStatus Status { get; set; } = GameStatus.Waiting;
		public int MaxPlayerCount { get; private set; }

		public BlackJackGame(ICardProvider cardProvider, BlackJackPlayer dealer, int maxPlayers)
		{
			Dealer = dealer ?? throw new ArgumentNullException(nameof(dealer));
			if (maxPlayers < 1)
			{
				throw new ArgumentOutOfRangeException(nameof(maxPlayers));
			}
			this.cardProvider = cardProvider ?? throw new ArgumentNullException(nameof(cardProvider));
			MaxPlayerCount = maxPlayers;
		}

		public void AddPlayer(BlackJackPlayer player)
		{
			_ = player ?? throw new ArgumentNullException(nameof(player));
			if (players.Count >= MaxPlayerCount)
			{
				throw new InvalidOperationException($"{player.Name} can NOT join game, The game Status is {Status}.");
			}
			players.Add(player);

			SetCurrentPlayerOnFirstPlayerAdd();
			AddDealerToListAfterFinalPlayer();
			SetReadyOnMaxPlayers();
		}

		public void SetPlayerStatusReady(string playerIdentifier)
		{
			var player = players.Where(p => p.Identifier.Equals(playerIdentifier)).SingleOrDefault();
			if (player == null) 
			{
				throw new ArgumentException(nameof(playerIdentifier), "Player Id NOT Found.");
			}
			player.Status = PlayerStatusTypes.Ready;
			SetGameInProgressOnAllPlayersReady();
		}

		public void PlayerHolds(string playerIdentifier, string handIdentifier)
		{
			_ = playerIdentifier ?? throw new ArgumentNullException(nameof(playerIdentifier));
			_ = handIdentifier ?? throw new ArgumentNullException(nameof(handIdentifier));
			if (CurrentPlayer.Identifier != playerIdentifier)
			{
				throw new ArgumentException(nameof(playerIdentifier), $"Please wait your turn, Current player is {CurrentPlayer.Name}");
			}

			var player = Players.Single(p => p.Identifier == playerIdentifier);
			player.Hold(handIdentifier);

			CurrentPlayer = player.Identifier == Dealer.Identifier
					? Players.First()
					: Players.SkipWhile(p => p.Identifier != playerIdentifier).Skip(1).Take(1).Single();
			SetGameCompleteOnAllPlayersComplete();
		}

		public void PlayerHits(string playerIdentifier, string handIdentifier)
		{
			_ = playerIdentifier ?? throw new ArgumentNullException(nameof(playerIdentifier));
			_ = handIdentifier ?? throw new ArgumentNullException(nameof(handIdentifier));
			if (CurrentPlayer.Identifier != playerIdentifier)
			{
				throw new ArgumentException(nameof(playerIdentifier), $"Please wait your turn, Current player is {CurrentPlayer.Name}");
			}

			Players.Single(p => p.Identifier == playerIdentifier)
					.Hit(handIdentifier, cardProvider.Cards(1).Single());
			SetGameCompleteOnAllPlayersComplete();
		}

		public void DealHands()
		{
			if (Status != GameStatus.Ready)
			{
				throw new ArgumentOutOfRangeException(nameof(Status), "Game Status Must be Ready to Deal Hands.");
			}

			var cardCount = players.Sum(p => p.Hands.Count());
			var cards = cardProvider.Cards(cardCount);

			players.ForEach(p => p.DealHands(cards.Take(p.Hands.Count() * 2)));
			
			Status = GameStatus.InProgress;
		}

		private void SetCurrentPlayerOnFirstPlayerAdd()
		{
			if (players.Count == 1)
			{
				CurrentPlayer = players.First();
			}
		}

		private void AddDealerToListAfterFinalPlayer()
		{
			if (players.Count == MaxPlayerCount)
			{
				Dealer.Status = PlayerStatusTypes.Ready;
				players.Add(Dealer);
			}
		}

		private void SetGameCompleteOnAllPlayersComplete() => Status =
				players.All(p => p.Status.Equals(PlayerStatusTypes.Complete))
				? GameStatus.Complete
				: GameStatus.InProgress;

		private void SetGameInProgressOnAllPlayersReady()
		{
			if (players.Count >= MaxPlayerCount && players.All(p => p.Status.Equals(PlayerStatusTypes.Ready)))
			{
				Status = GameStatus.Ready;
				Dealer.Status = PlayerStatusTypes.InProgress;
			}
			else
			{
				Status = GameStatus.Waiting;
			}
		}

		private void SetReadyOnMaxPlayers() => Status = players.Count >= MaxPlayerCount - 1
				? GameStatus.Ready
				: GameStatus.Waiting;
	}
}
