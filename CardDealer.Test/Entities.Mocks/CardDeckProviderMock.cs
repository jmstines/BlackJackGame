using System;
using System.Collections.Generic;
using System.Text;
using Entities;

namespace CardDealer.Tests.Entities.Mocks
{
  class CardDeckProviderMock : ICardDeckProvider
  {
    public List<Card> Deck => new CardDeckProvider().Deck;
  }
}
