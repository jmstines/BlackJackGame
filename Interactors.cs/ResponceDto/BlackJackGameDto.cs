using Entities.Enums;
using System.Collections.Generic;

namespace Interactors.ResponceDtos
{
	public class BlackJackGameDto
	{
		public List<BlackJackPlayerDto> Players { get; set; }
		public string CurrentPlayerId { get; set; }
		public string DealerId { get; set; }
		public GameStatus Status { get; set; }
	}
}
