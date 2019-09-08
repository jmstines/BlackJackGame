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
            Hand.Add(new BlackJackCard(card.Suit, card.Display, card.Description, FaceDown()));

            PointTotal = Hand.Sum(c => c.Value);
        }

        private bool FaceDown() => Hand.Any() ? false : true;
    }
}
