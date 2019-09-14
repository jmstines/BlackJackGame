using System;

namespace Entities
{
    public class BlackJackCard : ICard
    {

        public Suit Suit { get; private set; }
        public string Display { get; private set; }
        public string Description { get; private set; }
        public bool FaceDown { get; private set; }
        public int Value { get; set; }

        public BlackJackCard(Card card, bool faceDown, int value)
        {
            if (card == null)
            {
                throw new ArgumentNullException(nameof(card));
            }
            Suit = card.Suit;
            Display = card.Display;
            Description = card.Description;
            FaceDown = faceDown;
            Value = value > 0 ? value : throw new ArgumentOutOfRangeException(nameof(value));
        }
    }
}
