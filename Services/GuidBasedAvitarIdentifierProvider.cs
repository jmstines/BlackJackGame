using Entities.Interfaces;

namespace Interactors.Providers
{
	public class GuidBasedAvitarIdentifierProvider : GuidBasedIdentiferProviderBase, IAvitarIdentifierProvider
	{
		public string GenerateAvitar() => Generate(avitarIdentifierLength);
	}
}
