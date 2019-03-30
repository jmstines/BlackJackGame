using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
  class CardValue
  {
    public string Abbreviation { get; }
    public string Description { get; }
    public int Value { get; }

    public CardValue(string abbreviation, string description, int value)
    {
      Abbreviation = abbreviation ?? throw new ArgumentNullException(nameof(abbreviation));
      Description = description ?? throw new ArgumentNullException(nameof(description));
      Value = value;
    }

    public override bool Equals(object obj)
    {
      var value = obj as CardValue;
      return value != null &&
             Abbreviation == value.Abbreviation &&
             Description == value.Description &&
             Value == value.Value;
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(Abbreviation, Description, Value);
    }
  }
}
