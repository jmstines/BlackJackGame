using System;

namespace Entities
{
  public class Card
  {
    public readonly Suit Suit;
    public readonly string Display;
    public readonly string Description;

    public Card(Suit suit, string display, string description)
    {
      Suit = suit;
      Display = display ?? throw new ArgumentNullException(nameof(display));
      Description = description ?? throw new ArgumentNullException(nameof(description));
    }
  }
}
