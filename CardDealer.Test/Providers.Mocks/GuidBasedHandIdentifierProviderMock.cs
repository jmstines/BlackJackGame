using Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardDealer.Tests.Providers.Mocks
{
	class GuidBasedHandIdentifierProviderMock : IHandIdentifierProvider
	{
		public IEnumerable<string> GenerateHandIds(int count)
		{
			var ids = new List<string>() { "1234-123", "2345-234", "3456-345", "4567-456", "5678-567", "6789-678"};
			if (count == 0 | count > ids.Count)
			{
				throw new ArgumentOutOfRangeException(nameof(count));
			}		
			return ids.Take(count);
		}
	}
}
