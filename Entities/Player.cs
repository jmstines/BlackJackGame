using System;

namespace Entities
{
    public class Player
    {
		public string Name { get; private set; }
		public int PlayerIndex { get; set; }
		public Hand Hand { get; private set; } = new Hand();
		public PlayerStatus Status { get; set; } = PlayerStatus.InProgress;

		public Player(string name) => Name = name ?? throw new ArgumentNullException(nameof(name));

		public void AddCardToHand(BlackJackCard card) => Hand.AddCard(card);
	}
}
