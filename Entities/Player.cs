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
    public PlayerStatus Status { get; set; }

    public Player(string name)
    {
      Name = name ?? throw new ArgumentNullException(nameof(name));
      Status = PlayerStatus.InProgress;
      Hand = new List<BlackJackCard>();
			PointTotal = 0;
		}

    public void AddCardToHand(Card currentCard)
    {
			var card = currentCard ?? throw new ArgumentNullException(nameof(currentCard));
      bool faceDown = Hand.Any() ? false : true;
			var blackJackCard = new BlackJackCard(card.Suit, card.Display, card.Description, faceDown);
      Hand.Add(blackJackCard);

			PointTotal = Hand.Sum(c => c.Value);
		}
  }
}
