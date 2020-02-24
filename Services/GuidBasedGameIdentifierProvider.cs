using System;

namespace Interactors.Providers
{
	public class GuidBasedGameIdentifierProvider : GuidBasedIdentiferProviderBase, IGameIdentifierProvider
	{
		public string GenerateGameId() => Generate(gameIdentifierLength);
	}
}
