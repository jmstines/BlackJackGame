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

        public BlackJackCard(ICard card, bool faceDown, int value)
        {
            if (card == null)
            {
                throw new ArgumentNullException(nameof(card));
            }
            Suit = card.Suit;
            Display = card.Display ?? throw new ArgumentNullException(nameof(card.Display));
            if (Display.Trim() == string.Empty)
            {
                throw new ArgumentException(nameof(Display), $"{Display} can NOT be Empty!");
            }
            Description = card.Description ?? throw new ArgumentNullException(nameof(card.Description));
            FaceDown = faceDown;
            Value = value > 0 ? throw new ArgumentOutOfRangeException(nameof(value)) : value;
        }
    }
}
