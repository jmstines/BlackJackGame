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

		private static List<HandDto> MapHand(IEnumerable<Hand> hands, bool showAll)
		{
			List<HandDto> handDtos = new List<HandDto>();
			foreach (var hand in hands)
			{
				var dto = new HandDto
				{
					Cards = showAll
						? hand.Cards
						: hand.Cards.Where(c => c.FaceDown.Equals(false)),

					Identifier = hand.Identifier,
					Actions = hand.Actions,
					CardCount = hand.Cards.Count(),
					PointValue = hand.PointValue,
					Status = hand.Status
				};
				handDtos.Add(dto);
			}
			return handDtos;
		}
	}
}
