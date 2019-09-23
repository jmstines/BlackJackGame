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
        public List<PlayerAction> ValidActions { get; set; }
        public AIRules Rules { get; private set; }
        
        public Player(string name, AIRules rules)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Status = PlayerStatus.InProgress;
            Hand = new List<BlackJackCard>();
            PointTotal = 0;
            Rules = rules;
            ValidActions = new List<PlayerAction>() { PlayerAction.Draw, PlayerAction.Hold, PlayerAction.Split };
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
