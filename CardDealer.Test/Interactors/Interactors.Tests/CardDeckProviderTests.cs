using Entities.Enums;
using Entities.Interfaces;
using Interactors.Mocks;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Interactors.Tests
{
	public class CardDeckProviderTests
	{
		[SetUp]
		public void Setup()
		{

		}

		[Test]
		public void CreateNewDeck_CardCount_52()
		{
			List<ICard> deck = new CardDeckProviderMock().Deck.ToList();
			Assert.AreEqual(52, deck.Count);
		}

		[Test]
		public void CreateNewDeck_2ofClubs_FirstCard()
		{
			IEnumerable<ICard> deck = new CardDeckProviderMock().Deck;
			var card = deck.Single(c => c.Rank.Equals(CardRank.Two) && c.Suit.Equals(CardSuit.Clubs));

			Assert.AreEqual(deck.First(), card);
		}

		//    [Test]
		//    public void CreateCustomDeck_AceOfSpades_AllCards()
		//    {
		//        IEnumerable<CardRank> CardDetails = new List<CardRank>{
		//CardRank.Ace, CardRank.Ace,
		//CardRank.Ace, CardRank.Ace};

		//        IEnumerable<Card> deck = new CardDeckProvider(CardDetails).Deck;
		//        Assert.AreEqual(deck.Count(), 16);
		//        Assert.AreEqual(deck.Where(c => c.Rank.Equals(CardRank.Ace)).Count(), 16);
		//    }
	}
}