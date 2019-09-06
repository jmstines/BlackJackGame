using System;

namespace Entities
{
  public class Card : ICard
  {
    public Suit Suit { get; private set; }
    public string Display { get; private set; }
    public string Description { get; private set; }

    public Card(Suit suit, string display, string description)
    {
      Suit = suit;
      Display = display ?? throw new ArgumentNullException(nameof(display));
      Description = description ?? throw new ArgumentNullException(nameof(description));
    }
  }
}
