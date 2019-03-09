using System;

namespace Entities
{
  public class BlackJackCard
  {
    public readonly CardOrientation Orientation;
    public Card Card;
    public BlackJackCard(Card card, CardOrientation orientation)
    {
      Card = card ?? throw new ArgumentNullException(nameof(card));
      Orientation = orientation;
    }
  }
}
