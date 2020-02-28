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

		private readonly List<string> HandIds;
		private readonly Dictionary<string, Hand> hands = new Dictionary<string, Hand>();

		public BlackJackPlayer(KeyValuePair<string, Player> player, IEnumerable<string> handIds)
		{
			Name = player.Value.Name;
			PlayerIdentifier = player.Key;
			HandIds = handIds?.ToList() ?? throw new ArgumentNullException(nameof(handIds));

			AddHands();
			Status = PlayerStatusTypes.Waiting;
		}

		public void AddHands() => HandIds.ForEach(id => hands.Add(id, new Hand()));
	}
}
