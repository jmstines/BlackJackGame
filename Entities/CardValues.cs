using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
  class CardValues
  {
    private List<CardValue> Values = new List<CardValue>
    {
      new CardValue("2","2"), new CardValue("3", "3"), new CardValue("4", "4"),
      new CardValue("5", "5"), new CardValue("6", "6"), new CardValue("7", "7"),
      new CardValue("8", "8"), new CardValue("9", "9"), new CardValue("10", "10"),
      new CardValue("J", "Jack"), new CardValue("Q", "Queen"), new CardValue("K", "King"),
      new CardValue("A", "Ace")
    };
    public bool AddCardValue(CardValue value)
    {
      bool cardAdded = false;
      CardValue cardValue = value ?? throw new ArgumentNullException(nameof(value));
      if (!Values.Contains(cardValue))
      {
        Values.Add(cardValue);
        cardAdded = true;
      }
      return cardAdded;
    }
    public bool RemoveCard(CardValue value)
    {
      bool cardRemoved = false;
      CardValue cardValue = value ?? throw new ArgumentNullException(nameof(value));
      if (Values.Contains(cardValue))
      {
        Values.Remove(cardValue);
        cardRemoved = true;
      }
      return cardRemoved;
    }
  }
}
