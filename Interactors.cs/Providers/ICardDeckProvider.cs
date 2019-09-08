using System.Collections.Generic;
using Entities;

namespace Interactors.Providers
{
    public interface ICardDeckProvider
    {
        List<Card> Deck { get; }
    }
}