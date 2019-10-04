using System.Collections.Generic;

namespace Entities
{
	public class Hand
	{
		private readonly List<BlackJackCard> cards = new List<BlackJackCard>();

		public IEnumerable<BlackJackCard> Cards => cards;
		public int PointValue { get; set; }
		public void AddCard(BlackJackCard card) => cards.Add(card);
	}
}
