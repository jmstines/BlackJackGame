using System.Collections.Generic;

namespace Entities
{
	public class CardHand
	{
		public List<BlackJackCard> Cards { get; set; }
		public int PointValue { get; set; }

		public CardHand()
		{
			Cards = new List<BlackJackCard>();
			PointValue = 0;
		}
	}
}
