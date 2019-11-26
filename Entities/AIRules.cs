using System;
namespace Entities
{
	public struct AIRules
	{
		public bool IsAI { get; private set; }
		public int HoldValue { get; private set; }
		public AIRules(bool isAI, int holdValue)
		{
			this.IsAI = isAI;
			HoldValue = holdValue;
		}

		public override bool Equals(object obj)
		{
			return obj is AIRules rules &&
				   IsAI == rules.IsAI &&
				   HoldValue == rules.HoldValue;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(IsAI, HoldValue);
		}

		public static bool operator ==(AIRules left, AIRules right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(AIRules left, AIRules right)
		{
			return !(left == right);
		}
	}
}
