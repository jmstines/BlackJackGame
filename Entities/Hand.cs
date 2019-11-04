using System.Collections.Generic;

namespace Entities
{
	public class Hand
	{
		private readonly List<BlackJackCard> cards;

		public IEnumerable<HandActionTypes> Actions => new HandActions(Status, cards).Actions;
		public IEnumerable<BlackJackCard> Cards => cards;
		public int PointValue { get; private set; }
		public HandStatusTypes Status { get; private set; }

		public Hand()
		{
			cards = new List<BlackJackCard>();
			PointValue = 0;
			Status = HandStatusTypes.InProgress;
		}

		public void AddCard(BlackJackCard card)
		{
			cards.Add(card);
			PointValue = new HandValue(cards).Value;
			SetStatus(HandStatusTypes.InProgress);
		}

		public void SetStatus(HandStatusTypes status)
		{
			Status = PointValue > BlackJackConstants.BlackJack ? HandStatusTypes.Bust : status;
		}
	}
}
