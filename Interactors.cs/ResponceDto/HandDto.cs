﻿using Entities.Enums;
using Entities.Interfaces;
using System.Collections.Generic;

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
