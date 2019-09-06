using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
	public class CardDetail
	{
		public readonly string Description;
		public readonly string Display;

		public CardDetail(string display, string description)
		{
			Description = description ?? throw new ArgumentNullException(nameof(description));
			Display = display ?? throw new ArgumentNullException(nameof(display));
		}
	}
}
