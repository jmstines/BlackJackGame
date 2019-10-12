using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
	public class BlackJackPlayer
	{
		public string Name { get; private set; }
		public string PlayerId { get; private set; }
		public Hand Hand { get; private set; } = new Hand();
		public PlayerStatus Status { get; set; } = PlayerStatus.InProgress;
		public BlackJackPlayer(string id, Player player)
		{
			Name = player?.Name ?? throw new ArgumentNullException(nameof(player.Name));
			PlayerId = id ?? throw new ArgumentNullException(nameof(id));
		}

		public void AddCardToHand(BlackJackCard card) => Hand.AddCard(card);
	}
}
