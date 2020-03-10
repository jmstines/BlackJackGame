using System;
using System.Collections.Generic;
using Entities;
using Entities.Interfaces;

namespace Interactors.Providers
{
	public class HandProvider : CardProviderBase, IHandProvider
	{
		public override IEnumerable<ICard> Deck { get; set; }
		public override IRandomProvider RandomProvider { get; set; }

		public IEnumerable<Hand> Hands(IEnumerable<string> identifiers)
		{
			var hands = new List<Hand>();
			foreach(var id in identifiers)
			{
				hands.Add(new Hand(id));
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
