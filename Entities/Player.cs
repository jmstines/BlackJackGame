using System;

namespace Entities
{
    public class Player
    {
        public readonly string Name;
        public CardHand Hand { get; private set; }
        public PlayerStatus Status { get; set; }
        
        public Player(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Status = PlayerStatus.InProgress;
        }

        public void AddCardToHand(BlackJackCard card)
        {
            Hand.AddCard(card);
        }
    }
}
