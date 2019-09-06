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

    public BlackJackCard(Suit suit, string display, string description, bool faceDown)
    {
			Suit = suit;
			Display = display ?? throw new ArgumentNullException(nameof(display));
			Description = description ?? throw new ArgumentNullException(nameof(description));
			FaceDown = faceDown;
			SetCardValue();
    }

		public void ToggleValueOfAce()
		{
			if (!Display.Equals("A"))
			{
				throw new InvalidOperationException("Card Must be an Ace.");
			}
			Value = Value.Equals(1) ? 11 : 1;
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
