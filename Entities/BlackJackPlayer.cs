using Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Entities
{
	public class BlackJackPlayer
	{
		public string Name { get; private set; }
		public string PlayerIdentifier { get; private set; }
		public IDictionary<string, Hand> Hands => hands;
		public PlayerStatusTypes Status { get; set; }

		private readonly List<string> handIds;
		private readonly Dictionary<string, Hand> hands = new Dictionary<string, Hand>();

		public BlackJackPlayer(string id, Player player, IEnumerable<string> ids)
		{
			Name = player?.Name ?? throw new ArgumentNullException(nameof(player.Name));
			PlayerIdentifier = id ?? throw new ArgumentNullException(nameof(id));
			handIds = ids?.ToList() ?? throw new ArgumentNullException(nameof(ids));

			AddHands();
			Status = PlayerStatusTypes.InProgress;
		}

		public void AddHands() => handIds.ForEach(id => hands.Add(id, new Hand()));

		public override bool Equals(object obj)
		{
			return obj is BlackJackPlayer player &&
				   Name == player.Name &&
				   PlayerIdentifier == player.PlayerIdentifier;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(Name, PlayerIdentifier);
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
