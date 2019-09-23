using System;
using System.Collections.Generic;
using System.Linq;

namespace Entities
{
    public class Player
    {
        public readonly string Name;
        public List<BlackJackCard> Hand { get; private set; }
        public int PointTotal { get; set; }
        public PlayerStatus Status { get; set; }
        
        public Player(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Status = PlayerStatus.InProgress;
            Hand = new List<BlackJackCard>();
            PointTotal = 0;
        }

        public void AddCardToHand(BlackJackCard card)
        {
            if (card == null)
            {
                throw new ArgumentNullException(nameof(card));
            }
            Hand.Add(card);
        }
    }
}
