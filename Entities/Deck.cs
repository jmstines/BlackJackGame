using Entities.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Entities
{
	public class Deck : IEnumerable<ICard>
	{
		private readonly IEnumerable<CardSuit> Suits =
			new List<CardSuit> { CardSuit.Clubs, CardSuit.Diamonds, CardSuit.Hearts, CardSuit.Spades };
		private readonly IEnumerable<CardRank> CardRanks = new List<CardRank> {
			CardRank.Two, CardRank.Three, CardRank.Four, CardRank.Five, CardRank.Six, CardRank.Seven,
			CardRank.Eight, CardRank.Nine, CardRank.Ten, CardRank.Jack, CardRank.Queen, CardRank.King,
			CardRank.Ace };

		private readonly IEnumerable<ICard> Cards;

		public Deck()
		{
			Cards = BuildDefualtDeck();
		}

		public IEnumerator<ICard> GetEnumerator()
		{
			return Cards.GetEnumerator();
		}
		private IEnumerable<ICard> BuildDefualtDeck() =>
			Suits.SelectMany(suit => CardRanks.Select(rank => new Card(suit, rank) as ICard));

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
