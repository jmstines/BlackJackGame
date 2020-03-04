using System.Collections.Generic;
using Entities.Interfaces;

namespace Interactors.Providers
{
	public class GuidBasedIdentifierProviders : GuidBasedIdentiferProviderBase, IGuidBasedIdentifierProviders
	{
		public string GeneratePlayerId() => new GuidBasedPlayerIdentifierProvider().GeneratePlayerId();

		public IEnumerable<string> GenerateHandIds(int count) => new GuidBasedHandIdentifierProvider().GenerateHandIds(count);

		public string GenerateGameId() => new GuidBasedGameIdentifierProvider().GenerateGameId();

		public string GenerateAvitar() => new GuidBasedAvitarIdentifierProvider().GenerateAvitar();
	}
}
