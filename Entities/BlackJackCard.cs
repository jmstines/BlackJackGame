using System;

namespace Entities
{
  public class BlackJackCard
  {
    public readonly bool FaceDown;
		public readonly Suit Suit;
		public readonly string Display;
		public readonly string Description;
		public int Value;

    public BlackJackCard(Suit suit, string display, string description, bool faceDown)
    {
			Suit = suit;
			Display = display ?? throw new ArgumentNullException(nameof(display));
			Description = description ?? throw new ArgumentNullException(nameof(description));
			FaceDown = faceDown;
			SetCardValue();
    }

		private void SetCardValue()
		{
			switch (Display)
			{
				case "A":
					Value = 11;
					break;
				case "2":
					Value = 2;
					break;
				case "3":
					Value = 3;
					break;
				case "4":
					Value = 4;
					break;
				case "5":
					Value = 5;
					break;
				case "6":
					Value = 6;
					break;
				case "7":
					Value = 7;
					break;
				case "8":
					Value = 8;
					break;
				case "9":
					Value = 9;
					break;
				default:
					Value = 10;
					break;
			}
		}
  }
}
