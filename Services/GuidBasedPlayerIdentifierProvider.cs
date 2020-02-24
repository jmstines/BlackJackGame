using System;

namespace Interactors.Providers
{
	public class GuidBasedPlayerIdentifierProvider :  GuidBasedIdentiferProviderBase, IPlayerIdentifierProvider
	{
		public string GeneratePlayerId() => Generate(playerIdentifierLength);
	}
}
