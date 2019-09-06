using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
  class CardValue
  {
		public readonly string Abbreviation;
		public readonly string Description;

    public CardValue(string abbreviation, string description)
    {
      Abbreviation = abbreviation ?? throw new ArgumentNullException(nameof(abbreviation));
      Description = description ?? throw new ArgumentNullException(nameof(description));
    }

    public override bool Equals(object obj)
    {
			return obj is CardValue value &&
						 Abbreviation == value.Abbreviation &&
						 Description == value.Description;
		}

    public override int GetHashCode()
    {
      return HashCode.Combine(Abbreviation, Description);
    }
  }
}
