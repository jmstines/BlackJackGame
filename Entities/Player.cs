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
    public Outcome Status { get; set; }

    public Player(string name)
    {
      Name = name ?? throw new ArgumentNullException(nameof(name));
      Status = Outcome.InProgress;
      Hand = new List<BlackJackCard>();
    }

    public void DrawCard(Card card)
    {
      if (card == null)
      {
        throw new ArgumentNullException(nameof(card));
      }
      CardOrientation orientation = 
        Hand.Any() ? CardOrientation.FaceUp : CardOrientation.FaceDown;

      Hand.Add(new BlackJackCard(card, orientation));

      CalculatePointTotal();
    }

    private void CalculatePointTotal()
    {
      PointTotal = 0;
      PointTotal = Hand.Sum(c => c.Card.Value);
    }
  }
}
