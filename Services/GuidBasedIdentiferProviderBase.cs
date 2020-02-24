using System;

namespace Interactors.Providers
{
	public abstract class GuidBasedIdentiferProviderBase
	{
		internal const int playerIdentifierLength = 8;
		internal const int handIdentifierLength = 8;
		internal const int gameIdentifierLength = 8;
		internal const int avitarIdentifierLength = 14;
		internal string Generate(int length)
		{
			_ = length > 0 ? length : throw new ArgumentOutOfRangeException(nameof(length), "Guid Length Must be Longer than Zero Characters.");
			return Guid.NewGuid().ToString("N").Substring(0, length).ToUpper();
		}
	}
}
