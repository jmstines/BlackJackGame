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

		public void SetPlayerStatusReady(string id)
		{
			var player = players.Where(p => p.PlayerIdentifier.Equals(id)).Single();
			player.Status = PlayerStatusTypes.Ready;
			SetGameInProgressOnAllPlayersReady();
		}

		public void PlayerHolds() => CurrentPlayer = CurrentPlayer.Equals(players.Last())
		? Players.First()
		: Players.ElementAt(players.IndexOf(CurrentPlayer) + 1);

		public void PlayerHits(string handId, ICard card)
		{
			if (card.Rank == 0 || card.Suit == 0) throw new ArgumentOutOfRangeException(nameof(card));
			_ = handId ?? throw new ArgumentNullException(nameof(handId));
			if (CurrentPlayer.Hands.TryGetValue(handId, out Hand hand))
			{
				throw new ArgumentException(nameof(handId));
			}
			hand.AddCard(card);
		}

		public void DealHands()
		{
			var cardCount = Players.Sum(p => p.Hands.Count());
			var cards = cardProvider.Cards(cardCount);

			Players.ToList().ForEach(p => p.Hands.ToList().ForEach(h => h.Value.AddCardRange(cards.Take(2))));
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
