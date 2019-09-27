using System;
using System.Collections.Generic;
using System.Linq;

namespace Entities
{
	[Serializable]
	public class BlackJackGame
	{
		private readonly List<Player> players =  new List<Player>();
		private List<Card> deck = new List<Card>();

		public IEnumerable<Player> Players => players;
		public IEnumerable<Card> Deck => deck;
		public Player CurrentPlayer { get; set; }
		public GameStatus Status { get; set; }

		public BlackJackGame()
		{
			Status = GameStatus.Waiting;
			players = new List<Player>();
		}

		public void AddPlayer(Player player)
		{
			_ = player ?? throw new ArgumentNullException(nameof(player));
			if (players.Count >= BlackJackConstants.MaxPlayerCount)
			{
				throw new ArgumentOutOfRangeException(nameof(player), "Player Count must be less than 5 Players.");
			}
			players.Add(player);
			SetInProgressOnMaxPlayers();
		}

		private void SetInProgressOnMaxPlayers() =>
			Status = players.Count == BlackJackConstants.MaxPlayerCount ?
			GameStatus.InProgress : GameStatus.Waiting;

		public void DealHands(IEnumerable<Card> deck)
		{
			this.deck = deck?.ToList() ?? throw new ArgumentNullException(nameof(deck));
			int twoCardsPerPlayer = players.Count * 2;
			for (int i = 0; i < twoCardsPerPlayer; i++)
			{
				DrawCard();
				SetCurrentPlayerToNext();
			}
		}

		public void PlayerHolds() => SetCurrentPlayerToNext();

		public void PlayerDrawsCard() => DrawCard();

		private void DrawCard()
		{
			Card card = Deck.FirstOrDefault();
			deck.Remove(card);
			var hand = CurrentPlayer.Hand;
			hand.AddCard(new BlackJackCard(card, IsCardFaceDown));
			hand.PointValue = new CalculateHandValue(hand.Cards).Value();
		}

		private bool IsCardFaceDown => CurrentPlayer.Hand.Cards.Any() ? false : true;

		private void SetCurrentPlayerToNext() => CurrentPlayer = IsDealerCurrentPlayer ?
			Players.First() :
			Players.ElementAt(players.IndexOf(CurrentPlayer) + 1);

		private bool IsDealerCurrentPlayer => CurrentPlayer.Equals(Players.Last());

	}
}
