using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities;

namespace Interactors.Providers
{
	public class CardProviderRandom : ICardProviderRandom
	{
		//private readonly IEnumerable<CardSuit> Suits =
		//	new List<CardSuit> { CardSuit.Clubs, CardSuit.Diamonds, CardSuit.Hearts, CardSuit.Spades };
		//private readonly IEnumerable<CardRank> CardRanks = new List<CardRank> {
		//	CardRank.Two, CardRank.Three, CardRank.Four, CardRank.Five, CardRank.Six, CardRank.Seven,
		//	CardRank.Eight, CardRank.Nine, CardRank.Ten, CardRank.Jack, CardRank.Queen, CardRank.King,
		//	CardRank.Ace };

		private readonly IEnumerable<Card> Deck;

		public Card Card => Deck.RandomItem();
		public CardProviderRandom() => Deck = new Deck();

		//private IEnumerable<Card> BuildDefualtDeck() =>
		//	Suits.SelectMany(suit => CardRanks.Select(rank => new Card(suit, rank)));
	}
}
