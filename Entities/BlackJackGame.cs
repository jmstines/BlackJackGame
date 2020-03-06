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

		public BlackJackGame() { }

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
			
			SetCurrentPlayerOnFirstPlayerAdd(player);
			players.Add(player);

			AddDealerToListAfterFinalPlayer();
			SetReadyOnMaxPlayers();
		}

		public void SetPlayerStatusReady(string playerId)
		{
			var player = players.Where(p => p.PlayerIdentifier.Equals(playerId)).SingleOrDefault();
			if (player == null) 
			{
				throw new ArgumentException(nameof(playerId), "Player Id NOT Found.");
			}
			player.Status = PlayerStatusTypes.Ready;
			SetGameInProgressOnAllPlayersReady();
		}

		public void PlayerHolds(string playerId)
		{
			if (CurrentPlayer.PlayerIdentifier == playerId && CurrentPlayer != Dealer)
			{
				CurrentPlayer = CurrentPlayer.Equals(players.Last())
					? Players.First()
					: Players.ElementAt(players.IndexOf(CurrentPlayer) + 1);
			}
			else if (Dealer.PlayerIdentifier == playerId)
			{
				Status = GameStatus.Complete;
			}
		}

		public void PlayerHits(string playerId, string handId)
		{
			_ = playerId ?? throw new ArgumentNullException(nameof(playerId));
			_ = handId ?? throw new ArgumentNullException(nameof(handId));
			if (CurrentPlayer.PlayerIdentifier == playerId)
			{
				if (CurrentPlayer.Hands.TryGetValue(handId, out Hand hand))
				{
					{
						hand.AddCard(cardProvider.Cards(1).First());
					}
				}
				else
				{
					throw new ArgumentException(nameof(handId), "Hand Id Not Found.");
				}
			}
			else
			{
				throw new ArgumentException(nameof(playerId), "Player Hit Action out of Turn.");
			}
		}

		public void DealHands()
		{
			if (Status == GameStatus.InProgress) {
				var cardCount = Players.Sum(p => p.Hands.Count());
				var cards = cardProvider.Cards(cardCount);

				Players.ToList().ForEach(p => p.Hands.ToList().ForEach(h => h.Value.AddCardRange(cards.Take(2))));
			}
		}

		private void SetCurrentPlayerOnFirstPlayerAdd(BlackJackPlayer player)
		{
			if (!Players.Any())
			{
				CurrentPlayer = player;
			}
		}

		private void AddDealerToListAfterFinalPlayer()
		{
			if (players.Count == MaxPlayerCount)
			{
				players.Add(Dealer);
			}
		}

		private void SetGameInProgressOnAllPlayersReady() => Status = 
				players.Count == MaxPlayerCount
				&& players.All(p => p != Dealer
					&& p.Status.Equals(PlayerStatusTypes.Ready))
				? GameStatus.InProgress
				: GameStatus.Waiting;

		private void SetReadyOnMaxPlayers() => Status = players.Count >= MaxPlayerCount
				? GameStatus.Ready
				: GameStatus.Waiting;
	}
}
