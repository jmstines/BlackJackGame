using Entities.Enums;
using Entities.Interfaces;
using System.Collections.Generic;
using System;
using System.Linq;
using Entities.RepositoryDto;

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

		public BlackJackPlayer(AvitarDto avitar, IHandIdentifierProvider handIdProvider, int handCount)
		{
			Name = avitar.Name;
			Identifier = avitar.id;
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
			if (cards.Count() != hands.Count * 2)
			{
				throw new ArgumentOutOfRangeException("Card Count must equal TWO Per Hand.");
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

			hand.Hold();

			CheckForPlayerEndOfTurn();
		}

		public void Split(string handIdentifier)
		{
			var hand = Hands.SingleOrDefault(h => h.Identifier == handIdentifier);
			if (hand == null)
			{
				throw new ArgumentException(nameof(handIdentifier), $"{handIdentifier} Hand Identifier NOT Found.");
			}

			var ids = AddHands(2);
			var cards = hand.Split();
			hands.Remove(hand);
			var first = cards.First();
			var second = cards.Last();
			
			Hands.Single(h => h.Identifier == ids.First()).AddCard(first);
			Hands.Single(h => h.Identifier == ids.Last()).AddCard(second);
		}

		private List<string> AddHands(int handCount)
		{
			var ids = new List<string>();
			foreach (var id in handIdProvider.GenerateHandIds(handCount))
			{
				hands.Add(new Hand(id));
				ids.Add(id);
			}
			return ids;
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
