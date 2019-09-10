using System;

namespace Entities
{
    public class Card : ICard
    {
        public Suit Suit { get; private set; }
        public string Display { get; private set; }
        public string Description { get; private set; }

        public Card(Suit suit, string display, string description)
        {
            Suit = suit;
            Display = display ?? throw new ArgumentNullException(nameof(display));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            if (Display.Trim() == string.Empty)
            {
                throw new ArgumentException(nameof(Display), $"{Display} can NOT be Empty!");
            }
        }

        public override bool Equals(object obj) => obj is Card card &&
                   Suit == card.Suit &&
                   Display == card.Display &&
                   Description == card.Description;

        public override int GetHashCode() => HashCode.Combine(Suit, Display, Description);
    }
}
