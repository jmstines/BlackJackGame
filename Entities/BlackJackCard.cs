using System;

namespace Entities
{
    public class BlackJackCard
    {
        public Suit Suit { get; private set; }
        public CardRank Rank { get; private set; }
        public bool FaceDown { get; private set; }
        public BlackJackCard(Card card, bool faceDown)
        {
            Suit = card.Suit != 0 ? card.Suit : throw new ArgumentOutOfRangeException(nameof(card.Suit));
            Rank = card.Rank != 0 ? card.Rank : throw new ArgumentOutOfRangeException(nameof(card.Suit));
            FaceDown = faceDown;
        }
    }
}
