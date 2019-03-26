using System;

namespace Entities
{
  public class Card
  {
    public readonly Suit Suit;
    public readonly string Display;
    public readonly string Description;
    public readonly int Value;

    public Card(Suit suit, string display, string description, int value)
    {
      Suit = suit;
      Display = display ?? throw new ArgumentNullException(nameof(display));
      Description = description ?? throw new ArgumentNullException(nameof(description));
      if (value < 1)
      {
        throw new ArgumentOutOfRangeException(nameof(value), "Card values must be Greater than 0.");
      }
      else if (value > 11)
      {
        throw new ArgumentOutOfRangeException(nameof(value), "Card values must be Less than 12.");
      }

      Value = value;
    }
  }
}
