using System;
using System.Collections.Generic;
using System.Linq;

namespace Entities.ResponceDtos
{
	// TODO - Add Tests for the Mapper Class
	public class MapperBlackJackGameDto
	{
		private readonly BlackJackGame Game;
		public MapperBlackJackGameDto(BlackJackGame game)
		{
			Game = game ?? throw new ArgumentNullException(nameof(game));
		}

		public BlackJackGameDto Map(string playerId)
		{
			var dto = new BlackJackGameDto
			{
				Status = Game.Status,
				CurrentPlayerId = Game.CurrentPlayer.PlayerIdentifier
			};

			foreach (var player in Game.Players)
			{
				BlackJackPlayerDto playerDto;
				if (Game.Status != Enums.GameStatus.Complete)
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

		private BlackJackPlayerDto MapPlayer(BlackJackPlayer player, bool showAll)
		{
			return new BlackJackPlayerDto
			{
				Name = player.Name,
				PlayerIdentifier = player.PlayerIdentifier,
				Hands = MapHand(player.Hands, showAll),
				Status = player.Status
			};
		}

		private IDictionary<string, HandDto> MapHand(IDictionary<string, Hand> Hands, bool showAll)
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
					dto.PointValue = handValues.PointValue;
					dto.Status = handValues.Status;
				}
				else
				{
					dto.Actions = handValues.Actions;
					dto.Cards = handValues.Cards.Where(c => c.FaceDown.Equals(false));
					dto.PointValue = handValues.PointValue;
					dto.Status = handValues.Status;
				}
				handDtos.Add(hand.Key, dto);
			}
			return handDtos;
		}
	}
}
