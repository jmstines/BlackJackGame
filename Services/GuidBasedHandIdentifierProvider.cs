using System.Collections.Generic;

namespace Interactors.Providers
{
	public class GuidBasedHandIdentifierProvider : GuidBasedIdentiferProviderBase, IHandIdentifierProvider
	{
		public IEnumerable<string> GenerateHandIds(int count)
		{
			var ids = new List<string>();
			for (var i = 0; i < count; i++)
			{
				ids.Add(Generate(handIdentifierLength));
			}
			return ids;
		}
	}
}
