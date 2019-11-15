using System;

namespace Interactors.Providers
{
	public class GuidBasedAvitarIdentifierProvider : IAvitarIdentifierProvider
	{
		private const int identifierLength = 14;

		public string Generate() =>
			Guid.NewGuid().ToString("N").Substring(0, identifierLength).ToUpper();
	}
}
