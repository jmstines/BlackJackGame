using System;

namespace Interactors.Providers
{
	public class GuidBasedPlayerIdentifierProvider : IPlayerIdentifierProvider
	{
		private const int identifierLength = 8;

		public string Generate() =>
			Guid.NewGuid().ToString("N").Substring(0, identifierLength).ToUpper();
	}
}
