using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Entities
{
	public class Deck : IEnumerable<Card>
	{
		private readonly IEnumerable<CardSuit> Suits =
			new List<CardSuit> { CardSuit.Clubs, CardSuit.Diamonds, CardSuit.Hearts, CardSuit.Spades };
		private readonly IEnumerable<CardRank> CardRanks = new List<CardRank> {
			CardRank.Two, CardRank.Three, CardRank.Four, CardRank.Five, CardRank.Six, CardRank.Seven,
			CardRank.Eight, CardRank.Nine, CardRank.Ten, CardRank.Jack, CardRank.Queen, CardRank.King,
			CardRank.Ace };

		private readonly IEnumerable<Card> Cards;

		public Deck() => Cards = BuildDefualtDeck();

		public IEnumerator<Card> GetEnumerator()
		{
			return Cards.GetEnumerator();
		}
		private IEnumerable<Card> BuildDefualtDeck() =>
			Suits.SelectMany(suit => CardRanks.Select(rank => new Card(suit, rank)));

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
