using Entities.Interfaces;
using Entities.ResponceDtos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Entities
{
	// TODO - Add Tests for the Mapper Class
	public static class MapperBlackJackGameDto
	{
		public static BlackJackGameDto Map(BlackJackGame game, string playerId)
		{
			_ = game ?? throw new ArgumentNullException(nameof(game));
			var dto = new BlackJackGameDto
			{
				Status = game.Status,
				CurrentPlayerId = game.CurrentPlayer.Identifier,
				Players = new List<BlackJackPlayerDto>()
			};

			foreach (var player in game.Players)
			{
				BlackJackPlayerDto playerDto;
				if (game.Status != Enums.GameStatus.Complete)
				{
					var isCurrentPlayer = player.Identifier.Equals(playerId);
					playerDto = MapPlayer(player, isCurrentPlayer);
				}
				else
				{
					playerDto = MapPlayer(player, true);
				}
				dto.Players.Add(playerDto);
			}
			return dto;
		}

		private static BlackJackPlayerDto MapPlayer(BlackJackPlayer player, bool showAll)
		{
			return new BlackJackPlayerDto
			{
				Name = player.Name,
				PlayerIdentifier = player.Identifier,
				Hands = MapHand(player.Hands, showAll),
				Status = player.Status
			};
		}

		private static IDictionary<string, HandDto> MapHand(IEnumerable<Hand> Hands, bool showAll)
		{
			IDictionary<string, HandDto> handDtos = new Dictionary<string, HandDto>();
			foreach (var hand in Hands)
			{
				var dto = new HandDto();
				if (showAll)
				{
					dto.Actions = hand.Actions;
					dto.Cards = hand.Cards;
					dto.CardCount = hand.Cards.Count();
					dto.PointValue = hand.PointValue;
					dto.Status = hand.Status;
				}
				else
				{
					dto.Actions = hand.Actions;
					dto.Cards = hand.Cards.Where(c => c.FaceDown.Equals(false));
					dto.CardCount = hand.Cards.Count();
					dto.PointValue = hand.PointValue;
					dto.Status = hand.Status;
				}
				handDtos.Add(hand.Identifier, dto);
			}
			return handDtos;
		}
	}
}
