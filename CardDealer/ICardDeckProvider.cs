using System.Collections.Generic;
using Entities;

namespace CardDealer
{
  public interface ICardDeckProvider
  {
    List<Card> Deck { get; }
  }
}