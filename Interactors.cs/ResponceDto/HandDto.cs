using System;
using System.Collections.Generic;
using System.Text;
using Entities;
using Entities.Enums;
using Entities.Interfaces;

namespace Interactors.ResponceDtos
{
	public class HandDto
	{
		public IEnumerable<HandActionTypes> Actions { get; set; }
		public IEnumerable<IBlackJackCard> Cards { get; set; }
		public int PointValue { get; set; }
		public HandStatusTypes Status { get; set; }
	}
}
