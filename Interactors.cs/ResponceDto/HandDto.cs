using System;
using System.Collections.Generic;
using System.Text;
using Entities;

namespace Interactors.ResponceDtos
{
	public class HandDto
	{
		public IEnumerable<HandActionTypes> Actions { get; set; }
		public IEnumerable<BlackJackCard> Cards { get; set; }
		public int PointValue { get; set; }
		public HandStatusTypes Status { get; set; }
	}
}
