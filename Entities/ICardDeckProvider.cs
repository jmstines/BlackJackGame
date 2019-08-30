using System.Collections.Generic;
using Entities;

namespace Entities
{
  public interface ICardDeckProvider
  {
    List<Card> Deck { get; }
  }
}