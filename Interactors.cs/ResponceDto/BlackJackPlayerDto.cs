using Entities;
using Entities.Enums;
using System.Collections.Generic;

namespace Interactors.ResponceDtos
{
	public class BlackJackPlayerDto
	{
		public string Name { get; set; }
		public string PlayerIdentifier { get; set; }
		public IDictionary<string, HandDto> Hands { get; set; }
		public PlayerStatusTypes Status { get; set; }
	}
}
