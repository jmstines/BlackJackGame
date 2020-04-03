using Entities.Enums;
using Entities.Interfaces;
using System.Collections.Generic;

namespace Entities.ResponceDto
{
	public class HandDto
	{
		public string Identifier { get; set; }
		public IEnumerable<HandActionTypes> Actions { get; set; }
		public IEnumerable<IBlackJackCard> Cards { get; set; }
		public int CardCount { get; set; }
		public int PointValue { get; set; }
		public HandStatusTypes Status { get; set; }
	}
}
