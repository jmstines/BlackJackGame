using Entities;
using Interactors.ResponceDtos;
using System;
using System.Collections.Generic;

namespace Interactors.ResponseDtoMapper
{
	class BlackJackGameDtoMapper
	{
		private readonly BlackJackGame Game;
		public BlackJackGameDtoMapper(BlackJackGame game)
		{
			Game = game ?? throw new ArgumentNullException(nameof(game));
		}

		public BlackJackGameDto Map(bool showAll)
		{
			var dto = new BlackJackGameDto();
			dto.Status = Game.Status;
			var players = new List<BlackJackPlayerDto>();
			foreach (var player in Game.Players)
			{
				if (player.Equals(Game.CurrentPlayer))
				{
					var currentPlayer = new BlackJackPlayerMapper(player).Map(true);
					players.Add(currentPlayer);
					dto.CurrentPlayer = currentPlayer;
				}
				else if (player.Equals(Game.Dealer))
				{
					var dealer = new BlackJackPlayerMapper(player).Map(showAll);
					players.Add(dealer);
					dto.Dealer = dealer;
				}
				else
				{
					players.Add(new BlackJackPlayerMapper(player).Map(showAll));
				}
			}
			return dto;
		}
	}
}
