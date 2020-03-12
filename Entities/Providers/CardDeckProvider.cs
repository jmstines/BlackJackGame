using Entities.Enums;
using Entities.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Entities.Providers
{
	public class CardDeckProvider : ICardDeckProvider
	{
		private readonly IEnumerable<CardSuit> Suits =
			new List<CardSuit> { CardSuit.Clubs, CardSuit.Diamonds, CardSuit.Hearts, CardSuit.Spades };
		private readonly IEnumerable<CardRank> CardRanks = new List<CardRank> {
			CardRank.Two, CardRank.Three, CardRank.Four, CardRank.Five, CardRank.Six, CardRank.Seven,
			CardRank.Eight, CardRank.Nine, CardRank.Ten, CardRank.Jack, CardRank.Queen, CardRank.King,
			CardRank.Ace };

		public IEnumerable<ICard> Deck { get; private set; }
		public IEnumerable<ICard> ShuffledDeck { get => Deck.Shuffle(new RandomProvider()); }

		public CardDeckProvider() => Deck = BuildDefualtDeck();

		//public CardDeckProvider(IEnumerable<CardRank> cardRanks)
		//{
		//	CardRanks = cardRanks ?? throw new ArgumentNullException(nameof(cardRanks));
		//	Deck = BuildDefualtDeck();
		//}

		//public CardDeckProvider(IEnumerable<Card> deck) =>
		//	Deck = new List<Card>(deck) ?? throw new ArgumentNullException(nameof(deck));

		private IEnumerable<ICard> BuildDefualtDeck() =>
			 Suits.SelectMany(suit => CardRanks.Select(rank => new Card(suit, rank) as ICard));
	}
}
