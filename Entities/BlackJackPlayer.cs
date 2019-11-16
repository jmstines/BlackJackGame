using Entities.Enums;
using System;
using System.Collections.Generic;

namespace Entities
{
	public class BlackJackPlayer
	{
		public string Name { get; private set; }
		public string PlayerIdentifier { get; private set; }
		public Hand Hand { get; private set; }
		public PlayerStatusTypes Status { get; set; }
		public BlackJackPlayer(string id, Player player)
		{
			Name = player?.Name ?? throw new ArgumentNullException(nameof(player.Name));
			PlayerIdentifier = id ?? throw new ArgumentNullException(nameof(id));
			Hand = new Hand();
			Status = PlayerStatusTypes.InProgress;
		}

		public void AddCardToHand(BlackJackCard card) => Hand.AddCard(card);

		public override bool Equals(object obj)
		{
			return obj is BlackJackPlayer player &&
				   Name == player.Name &&
				   PlayerIdentifier == player.PlayerIdentifier &&
				   EqualityComparer<Hand>.Default.Equals(Hand, player.Hand) &&
				   Status == player.Status;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(Name, PlayerIdentifier, Hand, Status);
		}

		public static bool operator ==(BlackJackPlayer left, BlackJackPlayer right)
		{
			return EqualityComparer<BlackJackPlayer>.Default.Equals(left, right);
		}

		public static bool operator !=(BlackJackPlayer left, BlackJackPlayer right)
		{
			return !(left == right);
		}
	}
}
