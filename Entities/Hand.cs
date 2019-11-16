using Entities.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Entities
{
	public class Hand
	{
		private readonly List<IBlackJackCard> cards;

		public IEnumerable<HandActionTypes> Actions => HandActions.GetActions(cards);
		public IEnumerable<IBlackJackCard> Cards => cards;
		public int PointValue { get; private set; }
		public HandStatusTypes Status { get; private set; }

		public Hand()
		{
			cards = new List<IBlackJackCard>();
			PointValue = 0;
			Status = HandStatusTypes.InProgress;
		}

		public void AddCard(ICard card)
		{
			cards.Add(new BlackJackCard(card, IsCardFaceDown()));
			PointValue = HandValue.GetValue(cards);
			SetStatus(HandStatusTypes.InProgress);
		}

		public void SetStatus(HandStatusTypes status)
		{
			Status = PointValue > BlackJackConstants.BlackJack ? HandStatusTypes.Bust : status;
		}

		private bool IsCardFaceDown() => !Cards.Any();
	}
}
