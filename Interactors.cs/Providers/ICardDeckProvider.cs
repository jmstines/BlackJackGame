using System.Collections.Generic;
using Entities;

namespace Interactors.Providers
{
    public interface ICardDeckProvider
    {
        IEnumerable<Card> Deck { get; }
    }
}