using System.Collections.Generic;
using Entities;
using Entities.Interfaces;

namespace Interactors.Providers
{
    public interface ICardDeckProvider
    {
        IEnumerable<ICard> Deck { get; }
    }
}