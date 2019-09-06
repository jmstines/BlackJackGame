using System;
using System.Collections.Generic;
using System.Text;
using Entities;

namespace Interactors.Providers
{
	interface IShuffledDeckProvider
	{
		List<Card> ShuffledDeck { get; }
	}
}
