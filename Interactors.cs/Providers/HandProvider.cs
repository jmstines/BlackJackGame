using System;
using System.Collections.Generic;
using System.Linq;
using Entities;
using Entities.Interfaces;

namespace Interactors.Providers
{
	public class HandProvider : CardProviderBase, IHandProvider
	{
		public override IEnumerable<ICard> Deck { get; set; }
		public override IRandomProvider RandomProvider { get; set; }

		public IDictionary<string, Hand> Hands(IEnumerable<string> identifiers)
		{
			var hands = new Dictionary<string, Hand>();
			foreach(var id in identifiers)
			{
				hands.Add(id, new Hand());
			}
			return hands;
		}

		public HandProvider(IRandomProvider randomProvider, Deck deck)
		{
			Deck = deck ?? throw new ArgumentNullException(nameof(deck));
			RandomProvider = randomProvider ?? throw new ArgumentNullException(nameof(randomProvider));
		}
	}
}
