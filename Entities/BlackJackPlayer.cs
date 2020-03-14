using Entities.Enums;
using Entities.Interfaces;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Entities
{
	public class BlackJackPlayer
	{
		public string Name { get; private set; }
		public string Identifier { get; private set; }
		public IEnumerable<Hand> Hands => hands;
		public PlayerStatusTypes Status { get; set; }

		private readonly List<Hand> hands = new List<Hand>();
		private readonly IHandIdentifierProvider handIdProvider;

		public BlackJackPlayer(KeyValuePair<string, Avitar> avitar, IHandIdentifierProvider handIdProvider, int handCount)
		{
			Name = avitar.Value.Name;
			Identifier = avitar.Key;
			this.handIdProvider = handIdProvider ?? throw new ArgumentNullException(nameof(handIdProvider));
			if (handCount < 1)
			{
				throw new ArgumentOutOfRangeException(nameof(handCount));
			}

			AddHands(handCount);
			Status = PlayerStatusTypes.Waiting;
		}

		public void DealHands(IEnumerable<ICard> cards)
		{
			if (Status != PlayerStatusTypes.Ready)
			{
				throw new InvalidOperationException("Player Status Must be Waiting to Deal Hands.");
			}

			hands.ForEach(h => h.AddCardRange(cards.Take(2)));
			Status = PlayerStatusTypes.InProgress;
		}

		public void Hit(string handIdentifier, ICard card)
		{
			var hand = Hands.SingleOrDefault(h => h.Identifier == handIdentifier);
			if (hand == null)
			{
				throw new ArgumentException(nameof(handIdentifier), $"{handIdentifier} Hand Identifier NOT Found.");
			}

			if (hand.Status != HandStatusTypes.InProgress)
			{
				throw new InvalidOperationException($"{handIdentifier} Hand Status Must be In Progress to Hit.");
			}

			hand.AddCard(card);

			CheckForPlayerEndOfTurn();
		}

		public void Hold(string handIdentifier)
		{
			var hand = Hands.SingleOrDefault(h => h.Identifier == handIdentifier);
			if (hand == null)
			{
				throw new ArgumentException(nameof(handIdentifier), $"{handIdentifier} Hand Identifier NOT Found.");
			}

			if (hand.Status != HandStatusTypes.InProgress)
			{
				throw new InvalidOperationException($"{handIdentifier} Hand Status Must be In Progress to Hold.");
			}

			hand.SetStatus(HandStatusTypes.Hold);

			CheckForPlayerEndOfTurn();
		}

		private void AddHands(int handCount)
		{
			foreach (var id in handIdProvider.GenerateHandIds(handCount))
			{
				hands.Add(new Hand(id));
			}
		}

		private void CheckForPlayerEndOfTurn()
		{
			if (Hands.All(h => h.Status != HandStatusTypes.InProgress))
			{
				Status = PlayerStatusTypes.Complete;
			}
		}
	}
}
