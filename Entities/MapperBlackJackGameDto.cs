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
				CurrentPlayerId = game.CurrentPlayer.PlayerIdentifier,
				Players = new List<BlackJackPlayerDto>()
			};

			foreach (var player in game.Players)
			{
				BlackJackPlayerDto playerDto;
				if (game.Status != Enums.GameStatus.Complete)
				{
					var isCurrentPlayer = player.PlayerIdentifier.Equals(playerId);
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
				PlayerIdentifier = player.PlayerIdentifier,
				Hands = MapHand(player.Hands, showAll),
				Status = player.Status
			};
		}

		private static IDictionary<string, HandDto> MapHand(IDictionary<string, Hand> Hands, bool showAll)
		{
			IDictionary<string, HandDto> handDtos = new Dictionary<string, HandDto>();
			foreach (var hand in Hands)
			{
				var dto = new HandDto();
				var handValues = hand.Value;
				if (showAll)
				{
					dto.Actions = handValues.Actions;
					dto.Cards = handValues.Cards;
					dto.CardCount = handValues.Cards.Count();
					dto.PointValue = handValues.PointValue;
					dto.Status = handValues.Status;
				}
				else
				{
					dto.Actions = handValues.Actions;
					dto.Cards = handValues.Cards.Where(c => c.FaceDown.Equals(false));
					dto.CardCount = handValues.Cards.Count();
					dto.PointValue = handValues.PointValue;
					dto.Status = handValues.Status;
				}
				handDtos.Add(hand.Key, dto);
			}
			return handDtos;
		}
	}
}
