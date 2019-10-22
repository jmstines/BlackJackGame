using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
	public class BlackJackPlayer
	{
		public string Name { get; private set; }
		public string PlayerIdentifier { get; private set; }
		public Hand Hand { get; private set; }
		public PlayerStatus Status { get; set; }
		public BlackJackPlayer(string id, Player player)
		{
			Name = player?.Name ?? throw new ArgumentNullException(nameof(player.Name));
			PlayerIdentifier = id ?? throw new ArgumentNullException(nameof(id));
			Hand = new Hand();
			Status = PlayerStatus.InProgress;
		}

		public void AddCardToHand(BlackJackCard card) => Hand.AddCard(card);
	}
}
