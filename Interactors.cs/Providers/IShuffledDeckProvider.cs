using System.Collections.Generic;
using Entities;

namespace Interactors.Providers
{
    interface IShuffledDeckProvider
    {
        IEnumerable<Card> Shuffle();
    }
}
