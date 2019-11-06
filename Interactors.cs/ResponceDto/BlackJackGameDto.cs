using System.Collections.Generic;
using Entities;

namespace Interactors.ResponceDtos
{
	public class BlackJackGameDto
	{
		public IEnumerable<BlackJackPlayerDto> Players { get; set; }
		public BlackJackPlayerDto CurrentPlayer { get; set; }
		public BlackJackPlayerDto Dealer { get; set; }
		public GameStatus Status { get; set; }
	}
}
