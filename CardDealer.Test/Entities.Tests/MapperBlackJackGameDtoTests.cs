using CardDealer.Tests.Providers.Mocks;
using Entities.Enums;
using Entities.Interfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Entities.Tests
{
	class MapperBlackJackGameDtoTests
	{
		private readonly ICardProvider defautlCardProvider = new CardProviderMock();
		private readonly IHandIdentifierProvider HandIdentifierProvider = new GuidBasedHandIdentifierProviderMock();
		
		private readonly string PlayerTedId = "1234-ASDF";
		private BlackJackPlayer PlayerTed => new BlackJackPlayer(new KeyValuePair<string, Avitar>(
				PlayerTedId, new Avitar("Ted")), HandIdentifierProvider, 1);

		private readonly string DealerDataId = "10111001";
		private BlackJackPlayer DealerNamedData => new BlackJackPlayer(new KeyValuePair<string, Avitar>(
			   DealerDataId, new Avitar("Data")), HandIdentifierProvider, 1);

		[Test]
		public void Map_NullGame_ArgumentNullException() 
		{
			Assert.Throws<ArgumentNullException>(() => MapperBlackJackGameDto.Map(null, "12345679"));	
		}

		[Test]
		public void BeginGame1Player_InProgress_MapsCorrectly()
		{
			var game = new BlackJackGame(defautlCardProvider, DealerNamedData, 1);
			game.AddPlayer(PlayerTed);
			game.DealHands();
			var gameDto = MapperBlackJackGameDto.Map(game, PlayerTedId);

			var Ted = game.Players.Single(p => p.Identifier == PlayerTedId);
			var TedDto = gameDto.Players.Single(p => p.PlayerIdentifier == PlayerTedId);

			var dealer = game.Players.Single(p => p.Identifier == DealerDataId);
			var dealerDto = gameDto.Players.Single(p => p.PlayerIdentifier == DealerDataId);
			
			Assert.AreEqual(game.Players.Count(), gameDto.Players.Count);
			Assert.AreEqual(Ted.Hands.Count(), TedDto.Hands.Count);
			Assert.AreEqual(Ted.Hands.Single().Identifier, TedDto.Hands.Single().Identifier);
			Assert.AreEqual(Ted.Hands.Single().Actions, TedDto.Hands.Single().Actions);
			Assert.AreEqual(Ted.Hands.Single().PointValue, TedDto.Hands.Single().PointValue);
			Assert.AreEqual(2, TedDto.Hands.Single().CardCount);

			Assert.AreEqual(dealer.Hands.Count(), dealerDto.Hands.Count);
			Assert.AreEqual(dealer.Hands.Single().Identifier, dealerDto.Hands.Single().Identifier);
			Assert.AreEqual(dealer.Hands.Single().Actions, dealerDto.Hands.Single().Actions);
			Assert.AreEqual(dealer.Hands.Single().PointValue, dealerDto.Hands.Single().PointValue);
			Assert.AreEqual(2, dealerDto.Hands.Single().CardCount);
		}

		[Test]
		public void GameConplete1Player_Complete_MapsCorrectly()
		{
			var game = new BlackJackGame(defautlCardProvider, DealerNamedData, 1);
			game.AddPlayer(PlayerTed);
			game.DealHands();

			var TedHandKey = game.CurrentPlayer.Hands.Single().Identifier;

			game.PlayerHits(PlayerTedId, TedHandKey);
			game.PlayerHits(PlayerTedId, TedHandKey);
			game.PlayerHits(PlayerTedId, TedHandKey);
			game.PlayerHits(PlayerTedId, TedHandKey);
			game.PlayerHits(PlayerTedId, TedHandKey);
			game.PlayerHits(PlayerTedId, TedHandKey);
			game.PlayerHits(PlayerTedId, TedHandKey);
			game.PlayerHits(PlayerTedId, TedHandKey);
			game.PlayerHits(PlayerTedId, TedHandKey);
			game.PlayerHits(PlayerTedId, TedHandKey);
			game.PlayerHits(PlayerTedId, TedHandKey);
			var gameDto = MapperBlackJackGameDto.Map(game, PlayerTedId);

			var Ted = game.Players.Single(p => p.Identifier == PlayerTedId);
			var TedDto = gameDto.Players.Single(p => p.PlayerIdentifier == PlayerTedId);

			var dealer = game.Players.Single(p => p.Identifier == DealerDataId);
			var dealerDto = gameDto.Players.Single(p => p.PlayerIdentifier == DealerDataId);

			Assert.AreEqual(game.Players.Count(), gameDto.Players.Count);
			Assert.AreEqual(Ted.Hands.Count(), TedDto.Hands.Count);
			Assert.AreEqual(Ted.Hands.Single().Identifier, TedDto.Hands.Single().Identifier);
			Assert.AreEqual(Ted.Hands.Single().PointValue, TedDto.Hands.Single().PointValue);
			Assert.AreEqual(13, TedDto.Hands.Single().CardCount);

			Assert.AreEqual(dealer.Hands.Count(), dealerDto.Hands.Count);
			Assert.AreEqual(dealer.Hands.Single().Identifier, dealerDto.Hands.Single().Identifier);
			Assert.AreEqual(dealer.Hands.Single().PointValue, dealerDto.Hands.Single().PointValue);
			Assert.AreEqual(2, dealerDto.Hands.Single().CardCount);
		}
	}
}
