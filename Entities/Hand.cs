using System.Collections.Generic;

namespace Entities
{
	public class Hand
	{
		private readonly List<BlackJackCard> cards;

		public IEnumerable<HandActionTypes> Actions => HandActions.GetActions(Status, cards);
		public IEnumerable<BlackJackCard> Cards => cards;
		public int PointValue => HandValue.GetValue(cards);
		public HandStatusTypes Status
		{
			get => Status;
			set => Status = PointValue > BlackJackConstants.BlackJack ? HandStatusTypes.Bust : value;
		}

		public Hand()
		{
			cards = new List<BlackJackCard>();
			Status = HandStatusTypes.InProgress;
		}

		public void AddCard(BlackJackCard card)
		{
			cards.Add(card);
			Status = HandStatusTypes.InProgress;
		}
	}
}
