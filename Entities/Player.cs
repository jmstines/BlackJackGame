using System;
using System.Collections.Generic;
using System.Linq;

namespace Entities
{
  public class Player
  {
    public readonly string Name;
    public List<BlackJackCard> Hand { get; private set; }
    public int PointTotal { get; private set; }

    public Player(string name, List<Card> hand)
    {
      Name = name ?? throw new ArgumentNullException(nameof(name));
      if (hand == null) throw new ArgumentNullException(nameof(hand));
      if (hand.Count != 2) throw new ArgumentOutOfRangeException(nameof(hand));
      Hand = new List<BlackJackCard>
      {
        new BlackJackCard(hand[0], CardOrientation.FaceDown),
        new BlackJackCard(hand[1], CardOrientation.FaceUp)
      };

      CalculatePointTotal();
    }
    public void DrawCard(Card card)
    {
      if (card == null) throw new ArgumentNullException(nameof(card));
      Hand.Add(new BlackJackCard(card, CardOrientation.FaceUp));
      CalculatePointTotal();
    }

    private void CalculatePointTotal()
    {
      PointTotal = 0;
      PointTotal = 0;
      Hand.ForEach(c => PointTotal += c.Card.Value);
    }
  }
}
