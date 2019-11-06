using System;
using System.Collections.Generic;
using System.Text;
using Entities;

namespace Interactors.ResponceDtos
{
	public class BlackJackPlayerDto
	{
		public string Name { get; set; }
		public string PlayerIdentifier { get; set; }
		public HandDto Hand { get; set; }
		public PlayerStatusTypes Status { get; set; }
	}
}
