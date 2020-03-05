using Entities.Enums;
using Entities.Interfaces;
using System.Collections.Generic;
using System;

namespace Entities
{
	public class BlackJackPlayer
	{
		public string Name { get; private set; }
		public string PlayerIdentifier { get; private set; }
		public IDictionary<string, Hand> Hands => hands;
		public PlayerStatusTypes Status { get; set; }

		private readonly Dictionary<string, Hand> hands = new Dictionary<string, Hand>();
		private readonly IHandIdentifierProvider handIdProvider;

		public BlackJackPlayer(KeyValuePair<string, Avitar> avitar, IHandIdentifierProvider handIdProvider, int handCount)
		{
			Name = avitar.Value.Name;
			PlayerIdentifier = avitar.Key;
			this.handIdProvider = handIdProvider ?? throw new ArgumentNullException(nameof(handIdProvider));
			if (handCount < 1)
			{
				throw new ArgumentOutOfRangeException(nameof(handCount));
			}

			Status = PlayerStatusTypes.Waiting;
		}

		public void AddHands(int handCount)
		{
			foreach(var id in handIdProvider.GenerateHandIds(handCount))
			{
				Hands.Add(id, new Hand());
			}
		}
	}
}
